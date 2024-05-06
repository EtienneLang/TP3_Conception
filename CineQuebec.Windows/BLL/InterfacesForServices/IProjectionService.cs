﻿using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IDatabaseProjection
{
    void ReserverPlace(Projection projection, ObjectId idAbonne);
    Projection GetProjectionById(ObjectId id);
    List<Projection> ReadAvantPremieres();
}