using ProjetoAgenda.dominio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda.dominio.Entidades
{
    public class Agendamento: EntidadeBase
    {
        #region Propriedades
        public long IdDisponibilidade { get; private set; }
        public Disponibilidade Disponibilidade { get; private set; }
        public long IdUsuarioCliente { get; private set; }
        public Usuario UsuarioCliente { get; private set; }
        public bool Aceito { get; private set; } = false;
        public DateTime DataCadastro { get; set; }
        #endregion

        #region Metodos Privados
        private void AceitarAgendamento()
        {
            Aceito = true;
        }
        private void RegeitarAgendamento()
        {
            Aceito = false;
        }
        #endregion



        #region Construtores
        public Agendamento() { }

        private Agendamento(Disponibilidade agenda, Usuario usuarioCliente)
        {
            VincularAgenda(agenda);
            VincularUsuario(usuarioCliente);
            DataCadastro = DateTime.Now;
        }
        #endregion


        #region Métodos Públicos
        public void VincularAgenda(Disponibilidade disponibilidade)
        {
            if (disponibilidade is null || disponibilidade.Id <= 0)
                throw new ArgumentException("Agenda inválida");

            IdDisponibilidade = disponibilidade.Id;
        }

        public void VincularUsuario(Usuario usuario)
        {
            if (usuario is null || usuario.Id <= 0)
                throw new ArgumentException("Usuário inválido");

            IdUsuarioCliente = usuario.Id;
        }

        public static ResultadoGenerico<Agendamento> Criar(Disponibilidade agenda, Usuario usuarioCliente)
        {
            try
            {
                return new ResultadoGenerico<Agendamento>(
                true,
                "Agendamento criado com sucesso!",
                new Agendamento(agenda, usuarioCliente)
                );
            }
            catch (Exception ex)
            {
                return new ResultadoGenerico<Agendamento>(
                    false,
                    "Falha ao criar o Agendamento: " + ex.Message,
                    null
                );

            }

        }
        #endregion
    }



}
