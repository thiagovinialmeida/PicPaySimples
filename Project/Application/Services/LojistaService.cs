﻿using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class LojistaService : IUsuarioService
    {
        private readonly PicpaySimplesContext _context;
        public LojistaService (PicpaySimplesContext context) { _context = context; }

        public async Task CriarConta(string nome, string email, string senha, double saldo, string identidade)
        {
            if (identidade.Length == 14)
            {
                _context.Lojistas.Add(new Lojista(nome, email, senha, saldo, identidade));
                _context.SaveChanges();
            }
        }

        public async Task DeletarConta(Guid id)
        {
            try
            {
                var usuario = await ProcurarUsusario(id);
                if (await UsuarioExiste(id))
                {
                    _context.Lojistas.Remove(usuario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task EditarConta(Guid id)
        {
            try
            {
                var usuario = ProcurarUsusario(id);

                if (usuario != null)
                {
                    _context.Update(usuario);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException dbE)
            {
                Console.WriteLine(dbE.Message);
            }
        }
        public async Task<bool> VerificarExistencia(string email, string identidade)
        {
            if (EmailExiste(email))
            {
                //Caso o email não exista, verificar CPF
                if (IdentidadeExiste(identidade))
                {
                    //Caso o CPF não exista, pode criar novo usuario
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Lojista>> TodasContas()
        {
            return _context.Lojistas.ToList();
        }
        public async Task<Lojista> MostrarConta(Guid id)
        {
            return await ProcurarUsusario(id);
        }

        ////

        private async Task<Lojista> ProcurarUsusario(Guid id)
        {
            return _context.Lojistas.Find(id);
        }
        private async Task<bool> UsuarioExiste(Guid id)
        {
            return _context.Lojistas.Any(e => e.Id == id);
        }
        //Retorna TRUE se o CNPJ não existir
        private bool IdentidadeExiste(string identidade)
        {
            Lojista usuario = _context.Lojistas.FirstOrDefault(cpf => cpf.CNPJ == identidade);
            return usuario == null;
        }
        //Retorna TRUE se o email não existir
        private bool EmailExiste(string email)
        {
            Lojista usuario = _context.Lojistas.FirstOrDefault(e => e.Email == email);
            return usuario == null;
        }
    }
}
