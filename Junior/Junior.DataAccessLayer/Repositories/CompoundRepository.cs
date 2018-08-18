using AutoMapper;
using Junior.DataAccessLayer.Context;
using Junior.SharedModels.DomainModels;
using Junior.SharedModels.DtoModels;
using Serilog;
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
            Log.Information("GetAllCompounds triggered");

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
                Log.Error(ex, "GetAllCompounds");
                return null;
            }
        }

        public List<Compound> GetAllCompoundsByTypeId(Guid typeId)
        {
            Log.Information("GetAllCompoundsByTypeId triggered {typeId}", typeId);

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
                Log.Error(ex, "GetAllCompoundsByTypeId");
                return null;
            }
        }

        public List<CompoundType> GetAllCompoundTypes()
        {
            Log.Information("GetAllCompoundTypes triggered");

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
                Log.Error(ex, "GetAllCompoundTypes");
                return null;
            }
        }

        public List<Element> GetAllElements()
        {
            Log.Information("GetAllElements triggered");

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
                Log.Error(ex, "GetAllElements");
                return null;
            }
        }

        public List<CompoundElement> GetCompoundElements()
        {
            Log.Information("GetCompoundElements triggered");

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
                Log.Error(ex, "GetCompoundElements");
                return null;
            }
        }

        public List<CompoundElement> GetCompoundElementsByCompoundId(Guid id)
        {
            Log.Information("GetCompoundElementsByCompoundId triggered {id}", id);

            try
            {
                using (var context = new DatabaseContext())
                {
                    var entities = context.CompoundElements
                        .Include(ce => ce.Compound)
                        .Include(ce => ce.Element)
                        .Where(ce => ce.CompoundId == id)
                        .ToList();

                    return entities;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetCompoundElementsByCompoundId");
                return null;
            }
        }

        public bool CreateCompound(Compound entity)
        {
            Log.Information("CreateCompound triggered {@entity}", entity);

            try
            {
                using (var context = new DatabaseContext())
                {
                    context.Compounds.Add(entity);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CreateCompound");
                return false;
            }
        }

        public bool CreateCompoundElement(CompoundElementPartialDto entity)
        {
            Log.Information("CreateCompoundElement triggered {@entity}", entity);

            try
            {
                using (var context = new DatabaseContext())
                {
                    //First map to Compound object and save to Compounds table
                    var compound = Mapper.Map<Compound>(entity);

                    bool created = CreateCompound(compound);
                    if (!created)
                    {
                        return false;
                    }

                    //Then map to CompoundElement and save to binding CompoundElements table
                    foreach (var element in entity.Elements)
                    {
                        var compoundElement = Mapper.Map<CompoundElement>(element);
                        compoundElement.CompoundId = compound.Id;

                        context.CompoundElements.Add(compoundElement);
                    }

                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CreateCompoundElement");
                return false;
            }
        }

        public bool UpdateCompoundElement(CompoundElementPartialDto entity)
        {
            Log.Information("UpdateCompoundElement triggered {@entity}", entity);

            try
            {
                using (var context = new DatabaseContext())
                {
                    //First update compound
                    var compound = context.Compounds.Find(entity.CompoundId);
                    compound.Name = entity.Name;
                    compound.TypeId = entity.TypeId;

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
                Log.Error(ex, "UpdateCompoundElement");
                return false;
            }
        }

        public bool DeleteCompound(Guid id)
        {
            Log.Information("DeleteCompound triggered {id}", id);

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
            catch (Exception ex)
            {
                Log.Error(ex, "DeleteCompound");
                return false;
            }
        }
    }
}
