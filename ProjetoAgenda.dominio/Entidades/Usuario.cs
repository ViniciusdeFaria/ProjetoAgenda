using ProjetoAgenda.dominio.Helpers;
using System.Text.RegularExpressions;

namespace ProjetoAgenda.dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        #region propriedades 

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Administrador { get; private set; } = false;
        public bool Ativo { get; private set; } = true;

        private readonly List<Endereco> _enderecos = new();
        public IReadOnlyCollection<Endereco> Enderecos => _enderecos.AsReadOnly();

        #endregion

        #region metodos privados
        private void SetSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                throw new ArgumentException("Senha inválida (mínimo 6 caracteres)");

            // Pelo menos uma letra (maiúscula ou minúscula)
            if (!Regex.IsMatch(senha, @"[a-zA-Z]"))
                throw new ArgumentException("Senha inválida (deve conter pelo menos uma letra)");

            // Pelo menos um caractere especial
            if (!Regex.IsMatch(senha, @"[^a-zA-Z0-9]"))
                throw new ArgumentException("Senha inválida (deve conter pelo menos um caractere especial)");

            Senha = senha;
        }
        #endregion

        #region construtores
        public Usuario() { }
        private Usuario(string nome, string email, string senha, long? id)
        {
            if (id is not null)
                SetId(id.Value);

            SetNome(nome);

            SetEmail(email);

            SetSenha(senha);

            Ativo = true;

            Administrador = false;
        }




        #endregion

        #region metodos publicos
        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome inválido");

            Nome = nome.Trim();
        }


        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Email inválido");

            Email = email.Trim();

        }


        public ResultadoGenerico<bool> AlterarSenha(string senhaAtual, string novaSenha, string confirmacaoSenha)
        {
            if (Senha != senhaAtual)
                return new ResultadoGenerico<bool>(false, "Senha atual incorreta", false);

            if (novaSenha != confirmacaoSenha)
                return new ResultadoGenerico<bool>(false, "Confirmação de senha não confere!", false);

            SetSenha(novaSenha);

            return new ResultadoGenerico<bool>(true, "Senha alterado com sucesso!", true);
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void TornarAdministrador() => Administrador = true;


        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco is null)
                throw new ArgumentNullException("Endereço invalido");

            _enderecos.Add(endereco);
        }


        public static ResultadoGenerico<Usuario> Criar(string nome, string email, string senha, long? id)
        {
            try
            {
                if (id is not null)
                    return new ResultadoGenerico<Usuario>(
                        true,
                        "Usuario criado com sucesso.",
                        new Usuario(nome, email, senha, id)
                    );

                return new ResultadoGenerico<Usuario>(
                    true,
                    "Usuário criado com sucesso!",
                    new Usuario(nome, email, senha, null)
                );
            }
            catch (Exception ex)
            {
                return new ResultadoGenerico<Usuario>(
                    false,
                    "Falha ao criar o usuario: " + ex.Message,
                     null
                );
            }

            #endregion

        }
    }
}
