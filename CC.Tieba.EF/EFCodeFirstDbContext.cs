using CC.Tieba.EF.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Tieba.EF
{
    public class TiebaDbContext : DbContext
    {
        static object objLock = new object();
        public TiebaDbContext() : base()
        {
            lock (objLock)
                Database.EnsureCreated();
        }
        public DbSet<TiebaFloor> TiebaFloors { get; set; }
        public DbSet<TiebaFloorReply> TiebaFloorReplys { get; set; }
        public DbSet<TiebaGroup> TiebaGroups { get; set; }
        public DbSet<TiebaPost> TiebaPosts { get; set; }
        public DbSet<TiebaUser> TiebaUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //楼层表
            modelBuilder.Entity<TiebaFloor>()
                .HasIndex(p => p.UserName)
                .HasName("Index_FloorUserName");

            //楼层回复表
            modelBuilder.Entity<TiebaFloorReply>()
                .HasIndex(p => p.UserName)
                .HasName("Index_ReplyUserName");

            //贴吧表
            modelBuilder.Entity<TiebaGroup>()
                .HasIndex(p => p.Ba_Name)
                .HasName("Index_Ba_Name");

            //帖子表
            modelBuilder.Entity<TiebaPost>()
                .HasIndex(p => p.UserName)
                .HasName("Index_PostUserName");

            //用户表
            modelBuilder.Entity<TiebaUser>()
                .HasIndex(p => p.U_Name)
                .HasName("Index_U_Name");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=TiebaDB;uid=sa;pwd=123",
                builder =>
                {
                    builder.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: new int[] { 2 });
                });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
