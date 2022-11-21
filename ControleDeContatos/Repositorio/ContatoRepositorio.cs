using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _Context;    
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _Context = bancoContext;
        }

        public ContatoModel ListarPorId(int id) //BUSCAR O CONTATO POR ID
        {
            return _Context.Contatos.FirstOrDefault(c => c.Id == id);   
        }

        public List<ContatoModel> BuscarTodos()//BUSCAR NO BANCO DE DADOS
        {
            return _Context.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)//GRAVAR NO BANCO DE DADOS
        {
            _Context.Contatos.Add(contato);
            _Context.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);//PRECISA BUSCAR A INFORMAÇÃO NO BD PRA PODER ATUALIZAR

            if (contatoDB == null) throw new System.Exception("Houve um erro na atualização do contato");//SE NAO TIVER NINGUEM COM O ID INFORMADO, VAI DAR UMA MENSAGEM DE ERRO

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _Context.Contatos.Update(contatoDB);
            _Context.SaveChanges();
            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);
            if(contatoDB == null) throw new System.Exception("Houve um erro na deleção de contato!");
            _Context.Contatos.Remove(contatoDB);
            _Context.SaveChanges();
            return true;
           
        }
    }
}
