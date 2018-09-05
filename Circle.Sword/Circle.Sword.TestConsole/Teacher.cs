// ***********************************************************************
// Solution         : Circle.Sword
// Project          : Circle.Sword.TestConsole
// File             : Teacher.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Circle.Sword.Infrastructure.DapperExtensions;

namespace Circle.Sword.TestConsole
{
    [Table("Circle.Sword.Teachers", Schema = "dbo")]
    public class Teacher
    {
        [DbGenerated]
        public int Id { get; set; }

        [Key]
        public string TeacherId { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public long Amount { get; set; }
    }
}