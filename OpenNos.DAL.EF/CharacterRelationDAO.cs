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
using OpenNos.DAL.EF.DB;
using OpenNos.DAL.EF.Helpers;
using OpenNos.DAL.Interface;
using OpenNos.Data;
using OpenNos.Data.Enums;
using OpenNos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenNos.DAL.EF
{
    public class CharacterRelationDAO : MappingBaseDAO<CharacterRelation, CharacterRelationDTO>, ICharacterRelationDAO
    {
        #region Methods

        public DeleteResult Delete(long characterId, long relatedCharacterId)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    CharacterRelation relation = context.CharacterRelation.SingleOrDefault(c => c.CharacterId.Equals(characterId) && c.RelatedCharacterId.Equals(relatedCharacterId));

                    if (relation != null)
                    {
                        if (relation.RelationType != CharacterRelationType.Blocked)
                        {
                            CharacterRelation otherRelation = context.CharacterRelation.SingleOrDefault(c => c.CharacterId.Equals(relatedCharacterId) && c.RelatedCharacterId.Equals(characterId));
                            if (otherRelation != null)
                            {
                                context.CharacterRelation.Remove(otherRelation);
                            }
                        }
                        context.CharacterRelation.Remove(relation);
                        context.SaveChanges();
                    }

                    return DeleteResult.Deleted;
                }
            }
            catch (Exception e)
            {
                //Logger.Log.Error(String.Format(Language.Instance.GetMessageFromKey("DELETE_CHARACTER_ERROR"), characterSlot, e.Message), e);
                return DeleteResult.Error;
            }
        }

        public IList<CharacterRelationDTO> GetFriends(long characterId)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                return context.CharacterRelation
                    .Where(c => c.RelationType != CharacterRelationType.Blocked && c.CharacterId == characterId)
                    .OrderByDescending(c=>c.RelationType)
                    .ThenBy(c=>c.RelatedCharacterId)
                    .ToList()
                    .Select(c => _mapper.Map<CharacterRelationDTO>(c))
                    .ToList();
                
            }
        }

        public IList<CharacterRelationDTO> GetBlacklisted(long characterId)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                return context.CharacterRelation
                    .Where(c => c.RelationType == CharacterRelationType.Blocked && c.CharacterId == characterId)
                    .OrderBy(c=>c.RelatedCharacterId)
                    .ToList()
                    .Select(c => _mapper.Map<CharacterRelationDTO>(c))
                    .ToList();
            }
        }

        public SaveResult InsertOrUpdate(ref CharacterRelationDTO relation)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    long characterId = relation.CharacterId;
                    long relatedCharacterId = relation.RelatedCharacterId;
                    CharacterRelation entity = context.CharacterRelation.FirstOrDefault(c => c.CharacterId.Equals(characterId) && c.RelatedCharacterId.Equals(relatedCharacterId));

                    if (entity == null)
                    {
                        relation = Insert(relation, context);
                        return SaveResult.Inserted;
                    }
                    else
                    {
                        relation = Update(entity, relation, context);
                        return SaveResult.Updated;
                    }
                }
            }
            catch (Exception e)
            {
                //Logger.Log.Error(String.Format(Language.Instance.GetMessageFromKey("INSERT_ERROR"), character, e.Message), e);
                return SaveResult.Error;
            }
        }

        private CharacterRelationDTO Insert(CharacterRelationDTO relation, OpenNosContext context)
        {
            CharacterRelation entity = _mapper.Map<CharacterRelation>(relation);
            context.CharacterRelation.Add(entity);
            context.SaveChanges();
            return _mapper.Map<CharacterRelationDTO>(entity);
        }

        private CharacterRelationDTO Update(CharacterRelation entity, CharacterRelationDTO relation, OpenNosContext context)
        {
            if (entity != null)
            {
                _mapper.Map(relation, entity);
                context.SaveChanges();
            }

            return _mapper.Map<CharacterRelationDTO>(entity);
        }

        #endregion
    }
}