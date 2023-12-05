using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class IngredientsServices
    {
        private static IngredientsServices _ins;
        public static IngredientsServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new IngredientsServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<IngredientsDTO>> GetAllIngredients()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var allIngredientsList = (from nl in context.NGUYENLIEUx
                                              select new IngredientsDTO
                                              {
                                                  MANGUYENLIEU = nl.MANGUYENLIEU,
                                                  TENNGUYENLIEU = nl.TENNGUYENLIEU,
                                                  DONVI = nl.DONVI,
                                                  SOLUONGTRONGKHO = (int)nl.SOLUONGTRONGKHO,
                                              }).ToListAsync();

                    return await allIngredientsList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IngredientsDTO>> GetIngredientsInStock()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var allIngredientsList = (from nl in context.NGUYENLIEUx
                                              where nl.SOLUONGTRONGKHO >= 0
                                              select new IngredientsDTO
                                              {
                                                  MANGUYENLIEU = nl.MANGUYENLIEU,
                                                  TENNGUYENLIEU = nl.TENNGUYENLIEU,
                                                  DONVI = nl.DONVI,
                                                  SOLUONGTRONGKHO = (int)nl.SOLUONGTRONGKHO,
                                              }).ToListAsync();

                    return await allIngredientsList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IngredientsDTO>> GetIngredientsOutOfStock()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var allIngredientsList = (from nl in context.NGUYENLIEUx
                                              where nl.SOLUONGTRONGKHO <= 0
                                              select new IngredientsDTO
                                              {
                                                  MANGUYENLIEU = nl.MANGUYENLIEU,
                                                  TENNGUYENLIEU = nl.TENNGUYENLIEU,
                                                  DONVI = nl.DONVI,
                                                  SOLUONGTRONGKHO = (int)nl.SOLUONGTRONGKHO,
                                              }).ToListAsync();

                    return await allIngredientsList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
