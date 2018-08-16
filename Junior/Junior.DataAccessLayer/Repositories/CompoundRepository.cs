using AutoMapper;
using Junior.DataAccessLayer.Context;
using Junior.SharedModels.DomainModels;
using Junior.SharedModels.DtoModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Junior.DataAccessLayer.Repositories
{
    public class CompoundRepository
    {
        public List<Compound> GetAllCompounds()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.Compounds
                        .OrderBy(c => c.Name)
                        .Where(c => c.Deleted == false)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Compound> GetAllCompoundsByTypeId(Guid typeId)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.Compounds
                        .OrderBy(c => c.Name)
                        .Where(c => c.Deleted == false)
                        .Where(c => c.TypeId == typeId)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CompoundType> GetAllCompoundTypes()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.CompoundTypes
                        .OrderBy(c => c.Name)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Element> GetAllElements()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.Elements
                        .OrderBy(e => e.Name)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CompoundElement> GetCompoundElements()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.CompoundElements
                        .Include(ce => ce.Compound)
                        .Include(ce => ce.Element)
                        .OrderBy(ce => ce.Compound.Name)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CompoundElement> GetCompoundElementsByCompoundId(Guid id)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.CompoundElements
                        .Include(ce => ce.Compound)
                        .Where(ce => ce.CompoundId == id)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Guid CreateCompound(Compound entity)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    context.Compounds.Add(entity);
                    context.SaveChanges();

                    return entity.Id;
                }
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public bool CreateCompoundElement(CompoundElementDto entity)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    //First map to Compound object and save to Compounds table
                    var compound = Mapper.Map<Compound>(entity);

                    //Get resulting id
                    var compoundId = CreateCompound(compound);
                    if (compoundId.Equals(Guid.Empty))
                    {
                        return false;
                    }

                    //Then map to CompoundElement and save to binding CompoundElements table
                    foreach (var element in entity.Elements)
                    {
                        var compoundElement = Mapper.Map<CompoundElement>(element);
                        compoundElement.CompoundId = compoundId;

                        context.CompoundElements.Add(compoundElement);
                    }

                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCompoundElement(CompoundElementDto entity)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    //First update compound
                    var compound = context.Compounds.Find(entity.CompoundId);
                    compound.Name = entity.Name;
                    compound.TypeId = entity.TypeId;;

                    //Then update compound elements
                    foreach (var element in entity.Elements)
                    {
                        var compoundElement = context.CompoundElements.Find(element.CompoundElementId);
                        compoundElement.ElementId = element.Id;
                        compoundElement.ElementQuantity = element.Quantity;
                    }

                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCompound(Guid id)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entity = context.Compounds.Find(id);
                    entity.Deleted = true;
                    context.SaveChanges();

                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
