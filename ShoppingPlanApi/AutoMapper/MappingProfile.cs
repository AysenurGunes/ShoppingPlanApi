﻿using AutoMapper;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Models;

namespace ShoppingPlanApi.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingListAddDto, ShoppingList>();
           
        }
    }
}