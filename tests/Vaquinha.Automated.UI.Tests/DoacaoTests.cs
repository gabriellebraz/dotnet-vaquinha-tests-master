using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private string baseUrl = "https://vaquinha.azurewebsites.net/";

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl(baseUrl);
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));
			// _driver.FindElement(By.TagName("img")).GetAttribute("class").Contains("vaquinha-logo");

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl(baseUrl);
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			IWebElement valor = _driver.FindElement(By.Id("valor"));
			valor.SendKeys("10");

			// dados pessoais
			IWebElement campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);

			IWebElement campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoEmail.SendKeys(doacao.DadosPessoais.Email);

			IWebElement mensagemApoio = _driver.FindElement(By.Id("DadosPessoais_MensagemApoio"));
			mensagemApoio.SendKeys(doacao.DadosPessoais.MensagemApoio);



			// endereco de cobranca

			IWebElement textoEndereco = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			textoEndereco.SendKeys(doacao.EnderecoCobranca.TextoEndereco);

			IWebElement numeroEndereco = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			numeroEndereco.SendKeys(doacao.EnderecoCobranca.Numero);

			IWebElement cidadeCobranca = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			cidadeCobranca.SendKeys(doacao.EnderecoCobranca.Cidade);

			IWebElement cepEndereco = _driver.FindElement(By.Id("cep"));
			cepEndereco.SendKeys(doacao.EnderecoCobranca.CEP);
			
			IWebElement complementoEndereco = _driver.FindElement(By.Id("EnderecoCobranca_Complemento"));
			complementoEndereco.SendKeys(doacao.EnderecoCobranca.Complemento);

			IWebElement telefoneEndereco = _driver.FindElement(By.Id("telefone"));
			telefoneEndereco.SendKeys(doacao.EnderecoCobranca.Telefone);

			// dados para pagamento

			IWebElement nomeTPagamento = _driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
			nomeTPagamento.SendKeys(doacao.FormaPagamento.NomeTitular);

			IWebElement cartaoPagamento = _driver.FindElement(By.Id("cardNumber"));
			cartaoPagamento.SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);
			
			IWebElement validadeCartaoPagamento = _driver.FindElement(By.Id("validade"));
			validadeCartaoPagamento.SendKeys(doacao.FormaPagamento.Validade);

			IWebElement cvvPagamento = _driver.FindElement(By.Id("cvv"));
			cvvPagamento.SendKeys(doacao.FormaPagamento.CVV);

			//Assert
			_driver.Url.Should().Contain(baseUrl+"Doacoes/Create");
		}


		[Fact]
		public void DoacaoUI_DoacaoComDadosInvalidos()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoInvalida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl(baseUrl);
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			IWebElement valor = _driver.FindElement(By.Id("valor"));
			valor.SendKeys("10");

			// dados pessoais
			IWebElement campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);

			IWebElement campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoEmail.SendKeys(doacao.DadosPessoais.Email);

			IWebElement mensagemApoio = _driver.FindElement(By.Id("DadosPessoais_MensagemApoio"));
			mensagemApoio.SendKeys(doacao.DadosPessoais.MensagemApoio);



			// endereco de cobranca

			IWebElement textoEndereco = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			textoEndereco.SendKeys(doacao.EnderecoCobranca.TextoEndereco);

			IWebElement numeroEndereco = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			numeroEndereco.SendKeys(doacao.EnderecoCobranca.Numero);

			IWebElement cidadeCobranca = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			cidadeCobranca.SendKeys(doacao.EnderecoCobranca.Cidade);

			IWebElement cepEndereco = _driver.FindElement(By.Id("cep"));
			cepEndereco.SendKeys(doacao.EnderecoCobranca.CEP);
			
			IWebElement complementoEndereco = _driver.FindElement(By.Id("EnderecoCobranca_Complemento"));
			complementoEndereco.SendKeys(doacao.EnderecoCobranca.Complemento);

			IWebElement telefoneEndereco = _driver.FindElement(By.Id("telefone"));
			telefoneEndereco.SendKeys(doacao.EnderecoCobranca.Telefone);

			// dados para pagamento

			IWebElement nomeTPagamento = _driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
			nomeTPagamento.SendKeys(doacao.FormaPagamento.NomeTitular);

			IWebElement cartaoPagamento = _driver.FindElement(By.Id("cardNumber"));
			cartaoPagamento.SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);
			
			IWebElement validadeCartaoPagamento = _driver.FindElement(By.Id("validade"));
			validadeCartaoPagamento.SendKeys(doacao.FormaPagamento.Validade);

			IWebElement cvvPagamento = _driver.FindElement(By.Id("cvv"));
			cvvPagamento.SendKeys(doacao.FormaPagamento.CVV);

			IWebElement doar = _driver.FindElement(By.ClassName("btn-yellow"));
			doar.Click();

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create", because: "Foram informadas informacoes invalidas");

		}
	}
}