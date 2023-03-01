﻿using ShoppingPlanApi.Models;

namespace ShoppingPlanApi.Dtos
{
    public class ShoppingListDetailPutDto
    {

        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int MeasurementID { get; set; }
        public bool Done { get; set; }
        public string Notes { get; set; }

    }
}
