﻿namespace PlaceService.Application.Interfaces
{
    public interface IPaginationOptions
    {
        int Skip { get; }

        int Take { get; }
    }
}
