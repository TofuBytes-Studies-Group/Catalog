﻿namespace Catalog.Domain;

public interface IMenu
{
    Guid Id { get; set; }
    Guid RestaurantId { get; set; }
    Restaurant Restaurant { get; set; }
    List<Dish> Dishes { get; set; }
}