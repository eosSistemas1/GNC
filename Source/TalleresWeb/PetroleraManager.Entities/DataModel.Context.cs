﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetroleraManager.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataModelContext : DbContext
    {
        public DataModelContext()
            : base("name=DataModelContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BASEIMPONIBLE> BASEIMPONIBLE { get; set; }
        public DbSet<CLIENTES> CLIENTES { get; set; }
        public DbSet<COMPROBANTESCOMPRAS> COMPROBANTESCOMPRAS { get; set; }
        public DbSet<COMPROBANTESCOMPRASDETALLE> COMPROBANTESCOMPRASDETALLE { get; set; }
        public DbSet<COMPROBANTESCUMPLIDO> COMPROBANTESCUMPLIDO { get; set; }
        public DbSet<COMPROBANTESVENTAS> COMPROBANTESVENTAS { get; set; }
        public DbSet<COMPROBANTESVENTASDETALLE> COMPROBANTESVENTASDETALLE { get; set; }
        public DbSet<CONDICIONES> CONDICIONES { get; set; }
        public DbSet<CONTACTOS> CONTACTOS { get; set; }
        public DbSet<DEPOSITOS> DEPOSITOS { get; set; }
        public DbSet<ESTADOSCOMPROBANTES> ESTADOSCOMPROBANTES { get; set; }
        public DbSet<FABRICANTES> FABRICANTES { get; set; }
        public DbSet<IMPUESTOINTERNO> IMPUESTOINTERNO { get; set; }
        public DbSet<INVENTARIO> INVENTARIO { get; set; }
        public DbSet<INVENTARIODETALLE> INVENTARIODETALLE { get; set; }
        public DbSet<LOCALIDADES> LOCALIDADES { get; set; }
        public DbSet<MARCAS> MARCAS { get; set; }
        public DbSet<PERCEPCIONES> PERCEPCIONES { get; set; }
        public DbSet<PRODUCTOLOTE> PRODUCTOLOTE { get; set; }
        public DbSet<PRODUCTOS> PRODUCTOS { get; set; }
        public DbSet<PROVINCIAS> PROVINCIAS { get; set; }
        public DbSet<REGIVA> REGIVA { get; set; }
        public DbSet<ROLES> ROLES { get; set; }
        public DbSet<RUBROS> RUBROS { get; set; }
        public DbSet<SUCURSALES> SUCURSALES { get; set; }
        public DbSet<TIPOPRODUCTO> TIPOPRODUCTO { get; set; }
        public DbSet<TIPOSCOMPROBANTES> TIPOSCOMPROBANTES { get; set; }
        public DbSet<TIPOSDOCUMENTOS> TIPOSDOCUMENTOS { get; set; }
        public DbSet<TRANSPORTES> TRANSPORTES { get; set; }
        public DbSet<UNIDADES> UNIDADES { get; set; }
        public DbSet<USUARIOS> USUARIOS { get; set; }
        public DbSet<VENDEDORES> VENDEDORES { get; set; }
        public DbSet<ZONAS> ZONAS { get; set; }
        public DbSet<PRODUCTOSCOMPONENTES> PRODUCTOSCOMPONENTES { get; set; }
        public DbSet<OBLEASLIBRES> OBLEASLIBRES { get; set; }
        public DbSet<DOCUMENTOSCLIENTES> DOCUMENTOSCLIENTES { get; set; }
        public DbSet<PROVEEDORES> PROVEEDORES { get; set; }
        public DbSet<LOTES> LOTES { get; set; }
    }
}
