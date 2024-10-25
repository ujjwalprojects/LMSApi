using LMT.Api.DTOs;
using LMT.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LMT.Api.Data
{
    public class EFDBContext(DbContextOptions<EFDBContext> options) : DbContext(options)
    {

       
        public DbSet<M_BlockMunicipals> M_BlockMunicipals { get; set; }
        public DbSet<M_Districts> M_Districts { get; set; }
        public DbSet<M_JobRoles> M_JobRoles { get; set; }
        public DbSet<M_RegistrationActs> M_RegistrationActs { get; set; }
        public DbSet<M_States> M_States { get; set; }
        public DbSet<M_TaskPurposes> M_TaskPurposes { get; set; }
        public DbSet<M_WorkerTypes> M_WorkerTypes { get; set; }
        public DbSet<T_EstablishmentRegistrations> T_EstablishmentRegistrations { get; set; }
        public DbSet<T_TaskAllocationForms> T_TaskAllocationForms { get; set; }
        public DbSet<T_TaskAllocationSiteImages> T_TaskAllocationSiteImages { get; set; }
        public DbSet<T_WorkerRegistrations> T_WorkerRegistrations { get; set; }
        public DbSet<T_UniqueCodeRecords> T_UniqueCodeRecords { get; set; }
        public DbSet<T_UniqueCodeRecordsForWorkers> T_UniqueCodeRecordsForWorkers { get; set; }
        public DbSet<GetT_WorkerRegistrationDTO> GetT_WorkerRegistrationDTO { get; set; }
        public DbSet<GetTaskWorkersMapDTO> GetTaskWorkersMapDTO { get; set; }
        public DbSet<T_TaskWorkerMappers> T_TaskWorkerMappers { get; set; }
        public DbSet<GetEstablishmentCountDTO> GetEstablishmentCountDTOs { get; set; }
        public DbSet<GetWorkerRegistrationCountDTO> GetWorkerRegistrationCountDTOs { get; set; }
        public DbSet<GetTaskAllocationCountDTO> GetTaskAllocationCountDTOs { get; set; }
        public DbSet<GetWorkerRegistrationReportDTO> GetWorkerRegistrationReportDTOs { get; set; }
        public DbSet<GetEstablishmentRegistrationReportDTO> GetEstablishmentRegistrationReportDTOs { get; set; }
        public DbSet<GetRegisteredFieldOfficersDTO> GetRegisteredFieldOfficersDTOs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ignore any primary key mapping if DTO doesn't have a key
            modelBuilder.Entity<GetT_WorkerRegistrationDTO>().HasNoKey();
            modelBuilder.Entity<GetTaskWorkersMapDTO>().HasNoKey();
            modelBuilder.Entity<GetEstablishmentCountDTO>().HasNoKey();
            modelBuilder.Entity<GetWorkerRegistrationCountDTO>().HasNoKey();
            modelBuilder.Entity<GetTaskAllocationCountDTO>().HasNoKey();
            modelBuilder.Entity<GetWorkerRegistrationReportDTO>().HasNoKey();
            modelBuilder.Entity<GetEstablishmentRegistrationReportDTO>().HasNoKey();
            modelBuilder.Entity<GetRegisteredFieldOfficersDTO>().HasNoKey();

            // Other entity configurations
            base.OnModelCreating(modelBuilder);
        }
    }
}
