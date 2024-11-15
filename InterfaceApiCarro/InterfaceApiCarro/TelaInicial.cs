using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace InterfaceApiCarro
{
    public partial class GerenciadorDeVeículos : Form
    {
        private static readonly HttpClient cliente = new HttpClient();

        public GerenciadorDeVeículos()
        {
            InitializeComponent();
            ButtonSalvarAtt.Enabled = false;
            ButtonDeletar.Enabled = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.GetAsync("http://localhost:5225/carro?api_key=key_263b83291b9123762v1923b7210");
                if (resposta.IsSuccessStatusCode)
                {
                    var jsonResposta = await resposta.Content.ReadAsStringAsync();
                    var carros = JsonConvert.DeserializeObject<List<Carro>>(jsonResposta);
                    dataGridView1.DataSource = carros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
            }
        }

        private async void ButtonCarroID_Click(object sender, EventArgs e)
        {
            var carroId = TextCarroID.Text;

            if (string.IsNullOrEmpty(carroId))
            {
                MessageBox.Show("Por favor, insira o ID do veículo.");
                return;
            }

            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.GetAsync($"http://localhost:5225/carro/{carroId}?api_key=key_263b83291b9123762v1923b7210");

                if (resposta.IsSuccessStatusCode)
                {
                    var jsonResposta = await resposta.Content.ReadAsStringAsync();
                    var carro = JsonConvert.DeserializeObject<Carro>(jsonResposta);
                    dataGridView2.DataSource = new List<Carro> { carro };
                    if (carro == null)
                    {
                        MessageBox.Show("Veículo não encontrado");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o veículo: {ex.Message}");
            }
        }

        private void ButtonLimparCadastro_Click(object sender, EventArgs e)
        {
            TextMarca.Text = "";
            TextModelo.Text = "";
            TextAno.Text = "";
        }

        private async void ButtonSalvarCadastro_Click(object sender, EventArgs e)
        {
            string marca = TextMarca.Text;
            string modelo = TextModelo.Text;
            var ano = TextAno.Text;


            if (string.IsNullOrWhiteSpace(TextMarca.Text))
            {
                MessageBox.Show("Por favor, insira a marca do veículo!");
                TextMarca.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextModelo.Text))
            {
                MessageBox.Show("Por favor, insira o modelo do veículo!");
                TextModelo.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextAno.Text))
            {
                MessageBox.Show("Por favor, insira o ano do veículo!");
                TextAno.Focus();
                return;
            }

            var carro = new
            {
                Marca = marca,
                Modelo = modelo,
                Ano = ano
            };

            var json = JsonConvert.SerializeObject(carro);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.PostAsync("http://localhost:5225/carro?api_key=key_263b83291b9123762v1923b7210", conteudo);
                if (resposta.IsSuccessStatusCode)
                {
                    MessageBox.Show("Veículo cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Falha no cadastro, apenas o Administrador pode cadastrar!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao se conectar à API: {ex.Message}");
            }
        }

        private void ButtonLimparAtualizar_Click(object sender, EventArgs e)
        {
            TextIDAtt.Text = "";
            TextMarcaAtt.Text = "";
            TextModeloAtt.Text = "";
            TextAnoAtt.Text = "";
        }

        private async void ButtonBuscarAtt_Click(object sender, EventArgs e)
        {
            var carroId = TextIDAtt.Text;

            if (string.IsNullOrEmpty(carroId))
            {
                MessageBox.Show("Por favor, insira o ID do veículo.");
                return;
            }

            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.GetAsync($"http://localhost:5225/carro/{carroId}?api_key=key_263b83291b9123762v1923b7210");

                if (resposta.IsSuccessStatusCode)
                {
                    var jsonResposta = await resposta.Content.ReadAsStringAsync();
                    var carro = JsonConvert.DeserializeObject<Carro>(jsonResposta);

                    if (carro != null)
                    {
                        TextMarcaAtt.Text = carro.Marca;
                        TextModeloAtt.Text = carro.Modelo;
                        TextAnoAtt.Text = carro.Ano.ToString();
                        ButtonSalvarAtt.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Veículo não encontrado.");
                        TextMarcaAtt.Text = "";
                        TextModeloAtt.Text = "";
                        TextAnoAtt.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o veículo: {ex.Message}");
            }
        }

        private async void ButtonSalvarAtt_Click(object sender, EventArgs e)
        {
            var carroId = TextIDAtt.Text;
            string marca = TextMarcaAtt.Text;
            string modelo = TextModeloAtt.Text;
            var ano = TextAnoAtt.Text;

            if (string.IsNullOrWhiteSpace(TextIDAtt.Text))
            {
                MessageBox.Show("Por favor, insira o ID do veículo!");
                TextIDAtt.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextMarcaAtt.Text))
            {
                MessageBox.Show("Por favor, insira a marca do veículo!");
                TextMarca.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextModeloAtt.Text))
            {
                MessageBox.Show("Por favor, insira o modelo do veículo!");
                TextModelo.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextAnoAtt.Text))
            {
                MessageBox.Show("Por favor, insira o ano do veículo!");
                TextAno.Focus();
                return;
            }

            var carro = new
            {
                Marca = marca,
                Modelo = modelo,
                Ano = ano
            };

            var json = JsonConvert.SerializeObject(carro);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.PutAsync($"http://localhost:5225/carro/{carroId}?api_key=key_263b83291b9123762v1923b7210", conteudo);
                if (resposta.IsSuccessStatusCode)
                {
                    MessageBox.Show("Veículo alterado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Falha na alteração, apenas o Administrador pode alterar!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao se conectar à API: {ex.Message}");
            }

        }

        private async void ButtonDeletar_Click(object sender, EventArgs e)
        {
            var carroId = textIDDel.Text;

            if (string.IsNullOrEmpty(carroId))
            {
                MessageBox.Show("Por favor, insira o ID do veículo!");
                return;
            }
            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.DeleteAsync($"http://localhost:5225/carro/{carroId}?api_key=key_263b83291b9123762v1923b7210");

                if (resposta.IsSuccessStatusCode)
                {
                    MessageBox.Show("Veículo apagado com sucesso!");
                    textMarcaDel.Text = "";
                    textModeloDel.Text = "";
                    textAnoDel.Text = "";
                }
                else
                {
                    MessageBox.Show("Falha no delete, apenas o Administrador pode deletar!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o veículo: {ex.Message}");
            }

        }

        private async void buttonBuscarDel_Click(object sender, EventArgs e)
        {
            var carroId = textIDDel.Text;

            if (string.IsNullOrEmpty(carroId))
            {
                MessageBox.Show("Por favor, insira o ID do veículo.");
                return;
            }

            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.GetAsync($"http://localhost:5225/carro/{carroId}?api_key=key_263b83291b9123762v1923b7210");

                if (resposta.IsSuccessStatusCode)
                {
                    var jsonResposta = await resposta.Content.ReadAsStringAsync();
                    var carro = JsonConvert.DeserializeObject<Carro>(jsonResposta);

                    if (carro != null)
                    {
                        textMarcaDel.Text = carro.Marca;
                        textModeloDel.Text = carro.Modelo;
                        textAnoDel.Text = carro.Ano.ToString();
                        ButtonDeletar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Veículo não encontrado.");
                        textMarcaDel.Text = "";
                        textModeloDel.Text = "";
                        textAnoDel.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o veículo: {ex.Message}");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
                var resposta = await cliente.GetAsync("http://localhost:5225/account/user?api_key=key_263b83291b9123762v1923b7210");

                if (resposta.IsSuccessStatusCode)
                {
                    var jsonResposta = await resposta.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<List<User>>(jsonResposta);
                    dataGridView3.DataSource = user;
                }
                else
                {
                    MessageBox.Show("Apenas o Administrador pode ver registros do usuário!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar os usuários: {ex.Message}");
            }
        }

        private void GerenciadorDeVeículos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
