﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebLibrary.Model
{
    public partial class Cliente
    {
        public object nome;

        public Cliente()
        {
            LivroClienteEmprestimo = new HashSet<LivroClienteEmprestimo>();
        }

        [Key]
        [Display(Name = "Código")] // Data Annotations
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "CPF")] // Data Annotations
        [Column("CPF")]
        [StringLength(14)]
        [Unicode(false)]
        public string Cpf { get; set; }        
        [Required]
        [Column("nome")]
        [StringLength(100)]
        [Unicode(false)]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Endereço")] // Data Annotations
        [Column("endereco")]
        [StringLength(50)]
        [Unicode(false)]
        public string Endereco { get; set; }
        [Required]
        [Column("cidade")]
        [StringLength(50)]
        [Unicode(false)]
        public string Cidade { get; set; }
        [Required]
        [Column("bairro")]
        [StringLength(50)]
        [Unicode(false)]
        public string Bairro { get; set; }

        [InverseProperty("IdClienteNavigation")]
        public virtual ICollection<LivroClienteEmprestimo> LivroClienteEmprestimo { get; set; }
    }
}