using QRCoder.Xaml;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;

namespace ProxyQRCoder.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private DrawingImage _proxyQRCode;

        public string? ProxyString { get; set; }
        public DrawingImage ProxyQRCode 
        { 
            get => _proxyQRCode; 
            set => Set(ref _proxyQRCode, value, nameof(ProxyQRCode)); 
        }

        DrawingImage GenerateQR(string proxySting)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(proxySting);
            string base64Value = Convert.ToBase64String(bytes);

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode("http://" + base64Value, QRCodeGenerator.ECCLevel.Q);
            XamlQRCode qrCode = new XamlQRCode(qrCodeData);
            DrawingImage image = qrCode.GetGraphic(20);
            return image;
        }
        void GenerateProxyQR()
        {
            ProxyQRCode = GenerateQR(ProxyString);
        }

        public ICommand GenerateProxyQRCommand => new Command(x => GenerateProxyQR());
    }
}
