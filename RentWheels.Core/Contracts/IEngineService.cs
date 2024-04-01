﻿using RentWheels.Core.ViewModels.Engine;

namespace RentWheels.Core.Contracts
{
    public interface IEngineService
    {
        Task<bool> EngineExistsAsync(int engineId);

        Task<EngineDetailsViewModel> DetailsAsync(int id);
    }
}
