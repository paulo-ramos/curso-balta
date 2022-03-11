using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Paulo ramos","paulo@ramos.com.br");
        private readonly Product  _product  = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10, System.DateTime.UtcNow.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoValidoEleDeveGerarUmNumeroComOitoCaracteres()
        {
            
            var order = new Order(_customer, 0 , null);
            Assert.AreEqual(8,order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoOStatusDeleDeveSerAguardandoPagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPagamentoDoPedidoSeuStatusDeveSerAguardandoEntrega()
        {
            var order = new Order(_customer, 10 , _discount);
            order.AddItem(_product, 10);
            order.Pay(100);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);

        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPedidoCanceladoSeuStatusDeveSerCancelado()
        {
            var order = new Order(_customer, 100, _discount);
            order.AddItem(_product, 100);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);

        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoItemSemUmProdutoOMesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 100, _discount);
            order.AddItem(null, 100);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoItemComQuantidadeZeroOuMenorOMesmoNaoDeveSerAdicionado()
        {
           var order = new Order(_customer, 100, _discount);
           order.AddItem(_product, 0);
           Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);

        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoExpeiradoOTotaldoPedidoDeveSer60()
        {
            var discount = new Discount(10, System.DateTime.UtcNow.AddDays(-1));

            var order = new Order(_customer,10,discount);
            order.AddItem(_product,5);
            Assert.AreEqual(order.Total(),60);
            
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
        {
            var order = new Order(_customer,10,null);
            order.AddItem(_product,5);
            Assert.AreEqual(order.Total(),60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontode10OValorDoPedidoDeveSer50()
        {
            var order = new Order(_customer,10,_discount);
            order.AddItem(_product,5);
            Assert.AreEqual(order.Total(),50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmaTaxaDeEntregaDe1000OValorDoPedidoDeveSer1050()
        {
            var order = new Order(_customer,1000,_discount);
            order.AddItem(_product,6);
            Assert.AreEqual(order.Total(),1050);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPedidoSemClienteoMesmoDeveRetornarInvalido()
        {
            var order = new Order(null,10,_discount);
            order.AddItem(_product,1);
            Assert.AreEqual(order.IsValid,false);
        }

        
    }
}

