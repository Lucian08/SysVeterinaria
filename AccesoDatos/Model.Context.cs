﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VeterinariaEntities : DbContext
    {
        public VeterinariaEntities()
            : base("name=VeterinariaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Atencion> Atencion { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Detalle> Detalle { get; set; }
        public virtual DbSet<Especie> Especie { get; set; }
        public virtual DbSet<Mascota> Mascota { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    
        public virtual ObjectResult<ProcedureGetAtencionesCliente_Result> ProcedureGetAtencionesCliente(Nullable<int> cliente)
        {
            var clienteParameter = cliente.HasValue ?
                new ObjectParameter("Cliente", cliente) :
                new ObjectParameter("Cliente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcedureGetAtencionesCliente_Result>("ProcedureGetAtencionesCliente", clienteParameter);
        }
    
        public virtual ObjectResult<ProcedureGetAtencionesFechas_Result> ProcedureGetAtencionesFechas(Nullable<System.DateTime> inicio, Nullable<System.DateTime> termino)
        {
            var inicioParameter = inicio.HasValue ?
                new ObjectParameter("Inicio", inicio) :
                new ObjectParameter("Inicio", typeof(System.DateTime));
    
            var terminoParameter = termino.HasValue ?
                new ObjectParameter("Termino", termino) :
                new ObjectParameter("Termino", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcedureGetAtencionesFechas_Result>("ProcedureGetAtencionesFechas", inicioParameter, terminoParameter);
        }
    
        public virtual ObjectResult<ProcedureGetAtencionesMascota_Result> ProcedureGetAtencionesMascota(Nullable<int> mascota)
        {
            var mascotaParameter = mascota.HasValue ?
                new ObjectParameter("Mascota", mascota) :
                new ObjectParameter("Mascota", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcedureGetAtencionesMascota_Result>("ProcedureGetAtencionesMascota", mascotaParameter);
        }
    }
}
