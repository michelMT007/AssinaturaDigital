// See https://aka.ms/new-console-template for more information
using GdPicture14;
using System.Globalization;
using System.Drawing;

    string Nome="Michel A Costa";
    string Email = "michel.c@lyncas.net";
    string EstadoPais="Mato Grosso - Brasil";
    
    string Observacao= "Documentos cartoriais";
    string Assinatura = @"C:\Users\michel\source\repos\ConsoleApp1\ConsoleApp1\asssinatura.jpg";
    
    Console.WriteLine("Sistema para assinatura Digital para documentos digitais.");
    LicenseManager  LicenseManager = new LicenseManager();
    LicenseManager.RegisterKEY("0428418959767457973272048");
    //Assumimos que o GdPicture foi instalado e desbloqueado corretamente.
    string caption = "Assinatura digital";
    GdPicturePDF oGdPicturePDF = new GdPicturePDF();

    //Carregamento do documento PDF que deseja assinar.
    GdPictureStatus status = oGdPicturePDF.LoadFromFile(@"C:\Users\michel\source\repos\ConsoleApp1\ConsoleApp1\migracao-elastic.pdf", true);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O arquivo não pode ser carregado."+ caption);
    
    }
    //A ordem correta das etapas obrigatórias é importante.
    //Os passos obrigatórios são o passo #1 e o passo #2 e o último passo #5.
    //Passo 1: Configurando o certificado, seu arquivo de ID digital.
    status = oGdPicturePDF.SetSignatureCertificateFromP12(@"C:\Users\michel\source\repos\ConsoleApp1\ConsoleApp1\Pierre de Fermat.pfx", "1234");
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureCertificateFromP12() falhou com o status: " + status.ToString(), caption);
    
    }
    //Passo 2: Configurando as informações de assinatura. Pelo menos um parâmetro deve ser definido, outros podem ficar vazios.
    status = oGdPicturePDF.SetSignatureInfo(Nome, Observacao, EstadoPais, Email);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureInfo() falhou com o status: " + status.ToString()+caption);
    
    }
    //As etapas opcionais são a etapa #3 e a etapa #4.
    //Você pode selecionar entre essas opções o que você prefere. A ordem dessas etapas não é estritamente determinada.
    //Passo 3a: Configurando a localização da assinatura na página atual. Se esta etapa for omitida, a assinatura ficará invisível.
    status = oGdPicturePDF.SetSignaturePos(100, 200, 280, 100);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignaturePos() falhou com o status: " + status.ToString(), caption);
    
    }
    //Passo 3b: Configurando o texto a ser exibido dentro da caixa delimitadora da assinatura. O texto não será desenhado se a assinatura for invisível.
    status = oGdPicturePDF.SetSignatureText("", "", 12, Color.Navy, TextAlignment.TextAlignmentCenter, TextAlignment.TextAlignmentCenter, true);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureText() falhou com o status: " + status.ToString());
   
    }
    //Passo 3c: Configurando a imagem a ser exibida dentro da caixa delimitadora da assinatura. A imagem não será desenhada se a assinatura for invisível.
    string imageName = oGdPicturePDF.AddJpegImageFromFile(Assinatura);
    status = oGdPicturePDF.GetStat();
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método AddJpegImageFromFile() falhou com o status: " + status.ToString());
    
    }
    status = oGdPicturePDF.SetSignatureStampImage(imageName);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureStampImage() falhou com o status: " + status.ToString());
    
    }
    //Passo 3d: Configurando o ícone que representa a validade da assinatura.
    status = oGdPicturePDF.SetSignatureValidationMark(true);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureValidationMark() falhou com o status: " + status.ToString()+ caption);
    
    }
    //Passo 4a: Configurando o nível de certificação de assinatura.
    //PdfSignatureCertificationLevel.NotCertified representa assinaturas digitais de destinatário padrão (assinaturas de aprovação).
    //As outras enumerações representam as assinaturas de certificação que controlam a forma como os destinatários podem alterar o documento.
    status = oGdPicturePDF.SetSignatureCertificationLevel(PdfSignatureCertificationLevel.NotCertified);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureCertificationLevel() falhou com o status: " + status.ToString() + caption);
    
    }
    //Passo 4b: Configurando o algoritmo de hash.
    //O padrão usado (padrão) é SHA256, os outros são mais fortes.
    status = oGdPicturePDF.SetSignatureHash(PdfSignatureHash.SHA512);
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureHash() falhou com o status " + status.ToString()+ caption);
    
    }
    //Passo 4c: Configurando as informações de timestamp.
    var DataAssinatura = DateTime.Now.ToString("dd/MM/yyyy");
    status = oGdPicturePDF.SetSignatureTimestampInfo(DataAssinatura, "", "");
    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
    if (status != GdPictureStatus.OK)
    {
        Console.WriteLine("O método SetSignatureTimestampInfo() falhou com o status " + status.ToString());
    
    }
    //Passo 5: O último passo - assinar. Esta etapa deve ser a última. Todas as outras etapas opcionais podem ser feitas em qualquer ordem.
    status = oGdPicturePDF.ApplySignature(@"C:\Users\michel\source\repos\ConsoleApp1\ConsoleApp1\assinado.pdf", PdfSignatureMode.PdfSignatureModeAdobeCADES, true);
    if (status == GdPictureStatus.OK)
        Console.WriteLine("O documento foi assinado com sucesso e o arquivo foi salvo.");
    else
        Console.WriteLine("O método ApplySignature() falhou com o status: ");
    oGdPicturePDF.Dispose();
