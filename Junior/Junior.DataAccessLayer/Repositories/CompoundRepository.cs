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

        public List<CompoundElement> GetCompoundElementByCompoundId(Guid id)
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
                    //First save data to Compounds table
                    var compound = new Compound()
                    {
                        Name = entity.Name,
                        TypeId = entity.TypeId
                    };

                    var compoundId = CreateCompound(compound);
                    if (compoundId.Equals(Guid.Empty))
                    {
                        return false;
                    }

                    //Then save data to binding CompoundElements table
                    foreach (var element in entity.Elements)
                    {
                        context.CompoundElements.Add(new CompoundElement()
                        {
                            CompoundId = compoundId,
                            ElementId = element.ElementId,
                            ElementQuantity = element.ElementQuantity
                        });
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
                    //Map to Compound
                    var compound = new Compound()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        TypeId = entity.TypeId
                    };

                    context.Compounds.Attach(compound);
                    context.Entry(compound).State = EntityState.Modified;

                    //Map to CompoundElement
                    foreach (var element in entity.Elements)
                    {
                        var compoundElement = new CompoundElement()
                        {
                            Id = element.Id,
                            CompoundId = compound.Id,
                            ElementId = element.ElementId,
                            ElementQuantity = element.ElementQuantity
                        };
                        context.CompoundElements.Attach(compoundElement);
                        context.Entry(compoundElement).State = EntityState.Modified;
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
