using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebLibrary.Model
{
    public partial class Cliente
    {
        public Cliente()
        {
            LivroClienteEmprestimo = new HashSet<LivroClienteEmprestimo>();
        }

        [Key]
        [Column("id")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Required]
        [Column("CPF")]
        [StringLength(14)]
        [Unicode(false)]
        public string Cpf { get; set; }
        [Required]
        [Column("nome")]
        [StringLength(100)]
        [Unicode(false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [Column("endereco")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required]
        [Column("cidade")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [Required]
        [Column("bairro")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [InverseProperty("IdClienteNavigation")]
        public virtual ICollection<LivroClienteEmprestimo> LivroClienteEmprestimo { get; set; }
    }
}