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

        private readonly List<Servico> _servicos = new();
        public IReadOnlyCollection<Servico> Servicos => _servicos.AsReadOnly();
        private readonly List<Agendamento> _agendamentos = new();
        public IReadOnlyCollection<Agendamento> Agendamentos => _agendamentos.AsReadOnly();

        #endregion

        #region metodos privados
        private void SetSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                AdicionarNotificacao("Senha inválida (mínimo 6 caracteres)");

            // Pelo menos uma letra (maiúscula ou minúscula)
            if (!Regex.IsMatch(senha, @"[a-zA-Z]"))
                AdicionarNotificacao("Senha inválida (deve conter pelo menos uma letra)");


            // Pelo menos um caractere especial
            if (!Regex.IsMatch(senha, @"[^a-zA-Z0-9]"))
                AdicionarNotificacao("Senha inválida (deve conter pelo menos um caractere especial)");

            if (Valido)
                Senha = senha;
        }

        private bool EhAdministradorAtivo()
        {
            if (!Administrador)
                AdicionarNotificacao("Este Usuario não é um Administrador");

            if (Administrador && !Ativo)
                AdicionarNotificacao("Este Administrador não está ativo");

            return Valido;
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
            {
                AdicionarNotificacao("Nome inválido");
                return;
            }


            Nome = nome.Trim();
        }


        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                AdicionarNotificacao("Email inválido");
                return;
            }


            Email = email.Trim();

        }


        public ResultadoGenerico<bool> AlterarSenha(string senhaAtual, string novaSenha, string confirmacaoSenha)
        {
            if (string.IsNullOrWhiteSpace(senhaAtual) || string.IsNullOrWhiteSpace(novaSenha) || string.IsNullOrWhiteSpace(confirmacaoSenha))
            {
                AdicionarNotificacao("Para alterar senha, todos os campos são obrigatórios!");
                return new ResultadoGenerico<bool>(false, ObterMensagensDeErros(), false);
            }

            if (Senha != senhaAtual)
            {
                AdicionarNotificacao("Senha atual diferente!");
                return new ResultadoGenerico<bool>(false, ObterMensagensDeErros(), false);
            }


            if (novaSenha != confirmacaoSenha)
            {
                AdicionarNotificacao("Confirmação de senha não confere!");
                return new ResultadoGenerico<bool>(false, ObterMensagensDeErros(), false);
            }


            SetSenha(novaSenha);

            return new ResultadoGenerico<bool>(true, "Senha alterado com sucesso!", true);
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void TornarAdministrador() => Administrador = true;


        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco is null)
            {
                AdicionarNotificacao("Endereço informado errado!");
                return;
            }

            if (endereco.IdUsuario != Id)
            {
                AdicionarNotificacao("Endereço pertence a outro Usuario!");
                return;
            }

            _enderecos.Add(endereco);
        }


        public ResultadoGenerico<bool> AlterarStatus(Usuario usuarioAlvo, bool novoStatus)
        {
            if (!EhAdministradorAtivo())
                return new ResultadoGenerico<bool>(false, this.ObterMensagensDeErros(), false);

            if (ReferenceEquals(this, usuarioAlvo) || this.Id == usuarioAlvo.Id)
                usuarioAlvo.AdicionarNotificacao("Um usuario não pode gerenciar a si mesmo");

            if (usuarioAlvo.Administrador)
                usuarioAlvo.AdicionarNotificacao("Usuario alvo é um Administrador e não pode ser alterado!");

            if (novoStatus == usuarioAlvo.Ativo)
            {
                var mensagemErroAtivacao = novoStatus ? "Usuario alvo já está ativo" : "Usuario alvo ja está desativado";
                usuarioAlvo.AdicionarNotificacao(mensagemErroAtivacao);
            }

            if (!this.Valido || !usuarioAlvo.Valido)
            {
                var mensagemErros = string.Join(" | ", new[]
                {
                    this.ObterMensagensDeErros(),
                    usuarioAlvo.ObterMensagensDeErros()
                }.Where(x => !string.IsNullOrEmpty(x)));

                return new ResultadoGenerico<bool>(false, mensagemErros, false);
            }

            usuarioAlvo.Ativo = novoStatus;
            var msg = novoStatus ? "Usuario foi ativado com sucesso" : "Usuario foi desativado com sucesso";
            return new ResultadoGenerico<bool>(true, msg, true);
        }
        public static ResultadoGenerico<Usuario> Criar(string nome, string email, string senha, long? id)
        {
            var usuario = new Usuario(nome, email, senha, id);

            if (!usuario.Valido)
            {
                return new ResultadoGenerico<Usuario>(false, usuario.ObterMensagensDeErros(), null);
            }

            return new ResultadoGenerico<Usuario>(true, "Usuario criado com sucesso", usuario);


        }

        public void AdicionarServico(Servico servico)
        {
            if (servico is null)
            {
                AdicionarNotificacao("Serviço inválido.");
                return;
            }

            if (servico.IdUsuario != this.Id)
            {
                AdicionarNotificacao("Serviço pertence a outro usuário.");
                return;
            }



            
        }

        public void AdicionarAgendamento(Agendamento agendamento)
        {
            if (agendamento is null)
            {
                AdicionarNotificacao("Agendamento inválido.");
                return;
            }

            _agendamentos.Add(agendamento);
        }
        #endregion
    }
}    
