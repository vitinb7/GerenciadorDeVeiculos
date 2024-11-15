using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace InterfaceApiCarro
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient cliente = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextUsuario2.Focus();
        }

        private void ButtonLimparLogin_Click(object sender, EventArgs e)
        {
            TextUsuario.Text = "";
            TextPassword.Text = "";
            TextUsuario.Focus();
        }
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextUsuario.Focus();
        }

        private void ButtonLimparSignup_Click(object sender, EventArgs e)
        {
            TextUsuario2.Text = "";
            TextPassword2.Text = "";
            comboBoxRole.Text = "";
            TextUsuario2.Focus();
        }

        private async void ButtonSignup_Click(object sender, EventArgs e)
        {
            string usuario = TextUsuario2.Text;
            string password = TextPassword2.Text;
            string role = comboBoxRole.Text;

            if (string.IsNullOrWhiteSpace(TextUsuario2.Text))
            {
                MessageBox.Show("Por favor, insira seu nome de usuário!");
                TextUsuario2.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextPassword2.Text))
            {
                MessageBox.Show("Por favor, insira sua senha!");
                TextPassword2.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBoxRole.Text))
            {
                MessageBox.Show("Por favor, selecione seu cargo!");
                comboBoxRole.Focus();
                return;
            }

            var signupData = new
            {
                Usuario = usuario,
                Password = password,
                Role = role
            };

            var json = JsonConvert.SerializeObject(signupData);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var resposta = await cliente.PostAsync("http://localhost:5225/account/signup", conteudo);
                if (resposta.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cadastro realizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Falha no cadastro, Já existe alguém com esse usuário cadastrado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao se conectar à API: {ex.Message}");
            }
        }

        private async void ButtonEntrar_Click(object sender, EventArgs e)
        {
            string usuario = TextUsuario.Text;
            string password = TextPassword.Text;

            if (string.IsNullOrWhiteSpace(TextUsuario.Text))
            {
                MessageBox.Show("Por favor, insira seu nome de usuário!");
                TextUsuario.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextPassword.Text))
            {
                MessageBox.Show("Por favor, insira sua senha!");
                TextPassword.Focus();
                return;
            }

            var loginData = new
            {
                Usuario = usuario,
                Password = password
            };

            var json = JsonConvert.SerializeObject(loginData);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var resposta = await cliente.PostAsync("http://localhost:5225/account/login", conteudo);
                if (resposta.IsSuccessStatusCode)
                {
                    var respostaBody = await resposta.Content.ReadAsStringAsync();
                    var loginResposta = JsonConvert.DeserializeObject<LoginResposta>(respostaBody);
                    User.Token = loginResposta.Token;
                    MessageBox.Show("Login realizado com sucesso!");
                    var telaInicial = new GerenciadorDeVeículos();
                    telaInicial.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao se conectar à API: {ex.Message}");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                TextPassword.UseSystemPasswordChar = false;
            }
            else
            {
                TextPassword.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                TextPassword2.UseSystemPasswordChar = false;
            }
            else
            {
                TextPassword2.UseSystemPasswordChar = true;
            }
        }
    }
}

