using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace telaPedido
{
    public partial class Form1 : Form
    {
        // DEFININDO AS VARIAVEIS GLOBAIS
	public double total;
        public Form1()
        {
			// INICIALIZANDO OS COMPONENTES DA PÁGINA
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'adegaDataSet.Produto'. Você pode movê-la ou removê-la conforme necessário.
           
            this.produtoTableAdapter.Fill(this.adegaDataSet.Produto);

        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            int numero;
            if (int.TryParse(txtQuantidade.Text, out numero))
            {
                dgvProduto.Visible = true;
            }
            else
            {
                dgvProduto.Visible = false;
            }
        }

        private void dgvProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			// CONFIGURANDO O GRID DE VISUALIZAÇÃO DOS DADOS
            double valorUnitario = Convert.ToDouble(dgvProduto.Rows[e.RowIndex].Cells[1].Value);
            double quantidade = Convert.ToDouble(txtQuantidade.Text);
            txtDescricao.Text = Convert.ToString(dgvProduto.Rows[e.RowIndex].Cells[0].Value);
            txtValorUnitario.Text = Convert.ToString(valorUnitario);
            txtValorTotal.Text = Convert.ToString(valorUnitario * quantidade);
            txtDesconto.Text = "0";

        }

        private void dgvProduto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            double desconto, quantidade;

            if (txtDesconto.Text == "")
            {
                desconto = 0;
                txtDesconto.Text = "0";
            }
            else
            {
                if (double.TryParse(txtDesconto.Text, out desconto))
                {
                    if (double.TryParse(txtQuantidade.Text, out quantidade))
                    {
                        //double quantidade = Convert.ToDouble(txtQuantidade.Text);
                        double valorUnitario = Convert.ToDouble(txtValorUnitario.Text);
                        double total = (valorUnitario * quantidade) - desconto;
                        txtValorTotal.Text = Convert.ToString(total);
                    }


                }
                else
                {
                    MessageBox.Show("Desconto Inválido!");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblTotal.Text = Convert.ToString (Convert.ToDouble(lblTotal.Text) + Convert.ToDouble(txtValorTotal.Text));

           
            dgvItensPedido.Rows.Add(txtQuantidade.Text, txtDescricao.Text, txtValorUnitario.Text, cboFormaPagto.Text, txtDesconto.Text, txtValorTotal.Text);
            txtQuantidade.Text = "";
            txtDescricao.Text = "";
            txtValorUnitario.Text = "";
            cboFormaPagto.Text = "Dinheiro";
            txtDesconto.Text = "";
            txtValorTotal.Text = "";
            txtQuantidade.Focus();
        }

        private void dgvItensPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtQuantidade.Text = Convert.ToString(dgvItensPedido.Rows[e.RowIndex].Cells[0].Value);
            txtDescricao.Text = Convert.ToString(dgvItensPedido.Rows[e.RowIndex].Cells[1].Value);
            txtValorUnitario.Text = Convert.ToString(dgvItensPedido.Rows[e.RowIndex].Cells[2].Value);
            cboFormaPagto.Text = Convert.ToString(dgvItensPedido.Rows[e.RowIndex].Cells[3].Value);
            txtDesconto.Text = Convert.ToString(dgvItensPedido.Rows[e.RowIndex].Cells[4].Value);
            txtValorTotal.Text = Convert.ToString(dgvItensPedido.Rows[e.RowIndex].Cells[5].Value);
            lblTotal.Text = Convert.ToString(Convert.ToDouble(lblTotal.Text)-Convert.ToDouble(txtValorTotal.Text));
       
            dgvItensPedido.Rows.Remove(dgvItensPedido.CurrentRow); //.RowsRemoved(dgvItensPedido.Rows[e.RowIndex]);

        }

        private void btnGravarPedido_Click(object sender, EventArgs e)
        {
            string strCon = @"Data Source=DESKTOP-AI683CG;Initial Catalog=Adega;Persist Security Info=True;User ID=sa;Password=123456";
            SqlConnection conexao = new SqlConnection(strCon);

        }
    }
}
