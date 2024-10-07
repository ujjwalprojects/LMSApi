﻿using LMT.Api.DTOs;
using LMT.Api.Entities;
using System.Collections.Generic;

namespace LMT.Api.Interfaces
{
    public interface IEstablishmentRegistrationRepository
    {
        Task<List<T_EstablishmentRegistrations>> GetAllEstablishmentRegistrationsAsync();
        Task<List<T_EstablishmentRegistrations>> GetAllEstablishmentRegistrationsAsync(string? searchText);
        Task<T_EstablishmentRegistrations?> GetEstablishmentRegistrationByIdAsync(int establishmentRegistrationId);
        Task CreateEstablishmentRegistrationAsync(T_EstablishmentRegistrations establishmentRegistration);
        Task UpdateEstablishmentRegistrationAsync(T_EstablishmentRegistrations establishmentRegistration);
        Task DeleteEstablishmentRegistrationAsync(int establishmentRegistrationId);
        Task <IEnumerable<GetEstablishmentCountDTO>> GetEstablishmentCountDTOs(string? userId);
    }

}