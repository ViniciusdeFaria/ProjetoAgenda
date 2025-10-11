using ProjetoAgenda.dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda.Testes.Dominio
{
    public class UsuarioTestes
    {
        [Fact] 
        public void Criar_Deve_Sucesso_Quando_DadosValidos_SemId()
        {
            //range //action
            var resultado = Usuario.Criar("Joao", "Joao@dominio.com", "Test1@", null);

            //asserts
            Assert.True(resultado.Sucesso);
            Assert.NotNull(resultado.Dados);
            Assert.Equal("Usuario criado com sucesso", resultado.Mensagem);
            Assert.Equal("Joao", resultado.Dados.Nome);
            Assert.Equal("Joao@dominio.com", resultado.Dados.Email);
            Assert.Equal("Test1@", resultado.Dados.Senha);
            
        }
        [Theory]
        [InlineData("", "a@b.com", "Test1@", "Nome inválido")]
        [InlineData("João", "", "Test1@", "Email inválido")]
        [InlineData("João", "email-invalido", "Test1@", "Email inválido")]
        [InlineData("João", "joao@b.com", "123", "Senha inválida (mínimo 6 caracteres)")]
        [InlineData("João", "joao@b.com", "123456", "Senha inválida (deve conter pelo menos uma letra)")]
        [InlineData("João", "joao@b.com", "12345t", "Senha inválida (deve conter pelo menos um caractere especial)")]
        public void Criar_Deve_Falhar_Quando_DadosInvalidos(string nome, string email, string senha, string trechoMensagemEsperada)
        {
            var resultado = Usuario.Criar(nome, email, senha, null);

            Assert.False(resultado.Sucesso);
            Assert.Null(resultado.Dados);
            Assert.Contains(trechoMensagemEsperada, resultado.Mensagem);
        }
    }
}
