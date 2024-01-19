﻿namespace Catalog.DataAccess.Entities;

public class TypeEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}