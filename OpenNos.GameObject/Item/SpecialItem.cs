﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */

using OpenNos.Core;
using OpenNos.Domain;
using System;

namespace OpenNos.GameObject
{
    public class SpecialItem : Item
    {
        #region Methods

        public override void Use(ClientSession Session, ref Inventory Inv, bool DelayUsed = false)
        {
            switch (Effect)
            {
                case 650: //wings
                    SpecialistInstance specialistInstance = Session.Character.EquipmentList.LoadBySlotAndType<SpecialistInstance>((byte)EquipmentType.Sp, (byte)InventoryType.Equipment);
                    if (Session.Character.UseSp && specialistInstance != null)
                    {
                        if (!DelayUsed)
                        {
                            Session.Client.SendPacket($"qna #u_i^1^{Session.Character.CharacterId}^{Inv.Type}^{Inv.Slot}^3 {Language.Instance.GetMessageFromKey("ASK_WINGS_CHANGE")}");
                        }
                        else
                        {
                            specialistInstance.Design = (byte)EffectValue;
                            Session.Character.MorphUpgrade2 = EffectValue;
                            Session.Client.SendPacket(Session.Character.GenerateCMode());
                            Session.Client.SendPacket(Session.Character.GenerateStat());
                            Session.Client.SendPacket(Session.Character.GenerateStatChar());
                            Inv.ItemInstance.Amount--;
                            if (Inv.ItemInstance.Amount > 0)
                                Session.Client.SendPacket(Session.Character.GenerateInventoryAdd(Inv.ItemInstance.ItemVNum, Inv.ItemInstance.Amount, Inv.Type, Inv.Slot, 0, 0, 0, 0));
                            else
                            {
                                Session.Character.InventoryList.DeleteFromSlotAndType(Inv.Slot, Inv.Type);
                                Session.Client.SendPacket(Session.Character.GenerateInventoryAdd(-1, 0, Inv.Type, Inv.Slot, 0, 0, 0, 0));
                            }
                        }
                    }
                    else
                        Session.Client.SendPacket(Session.Character.GenerateMsg(Language.Instance.GetMessageFromKey("NO_SP"), 0));
                    break;

                case 1000: // vehicles
                    SpecialistInstance sp = Session.Character.EquipmentList.LoadBySlotAndType<SpecialistInstance>((byte)EquipmentType.Sp, (byte)InventoryType.Equipment);
                    if (!DelayUsed && Session.Character.IsVehicled == false)
                    {
                        Session.Client.SendPacket(Session.Character.GenerateDelay(3000, 3, $"#u_i^1^{Session.Character.CharacterId}^{Inv.Type}^{Inv.Slot}^2"));
                    }
                    else
                    {
                        Session.Client.SendPacket("pinit 0");
                        if (Session.Character.IsVehicled == false)
                        {
                            Session.Character.IsVehicled = true;
                            if (sp != null)
                            {
                                Session.Character.MorphUpgrade = 0;
                                Session.Character.MorphUpgrade2 = 0;
                            }
                            Session.Character.Morph = Morph + Session.Character.Gender;
                            Session.Character.LastSpeed = Session.Character.Speed;
                            Session.Character.Speed = Speed;
                            Session.CurrentMap?.Broadcast(Session.Character.GenerateEff(196));
                        }
                        else
                        {
                            Session.Character.IsVehicled = false;
                            Session.CurrentMap?.Broadcast(Session.Character.GenerateCMode());
                            Session.Character.Speed = Session.Character.LastSpeed;
                            if (Session.Character.UseSp)
                            {
                                if (sp != null)
                                {
                                    Session.Character.Morph = ServerManager.GetItem(sp.ItemVNum).Morph;
                                    Session.Character.MorphUpgrade = sp.Upgrade;
                                    Session.Character.MorphUpgrade2 = sp.Design;
                                }
                            }
                            else
                            {
                                Session.Character.Morph = 0;
                            }
                        }
                        Session.CurrentMap?.Broadcast(Session.Character.GenerateCMode());
                        Session.Client.SendPacket(Session.Character.GenerateCond());
                    }
                    break;

                default:
                    Logger.Log.Warn(String.Format(Language.Instance.GetMessageFromKey("NO_HANDLER_ITEM"), this.GetType().ToString()));
                    break;
            }
        }

        #endregion
    }
}