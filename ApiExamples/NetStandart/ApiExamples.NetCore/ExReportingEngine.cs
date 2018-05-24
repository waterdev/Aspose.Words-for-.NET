﻿// Copyright (c) 2001-2017 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using ApiExamples.NetCore.TestData.TestBuilders;
using ApiExamples.NetCore.TestData.TestClasses;
using ApiExamples.TestData;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Reporting;
using NUnit.Framework;
using SkiaSharp;

namespace ApiExamples.NetCore
{
    [TestFixture]
    public class ExReportingEngine : ApiExampleBase
    {
        private readonly string mImage = ImageDir + "Test_636_852.gif";
        private readonly string mDocument = MyDir + "ReportingEngine.TestDataTable.docx";

        [Test]
        public void SimpleCase()
        {
            Document doc = DocumentHelper.CreateSimpleDocument("<<[s.Name]>> says: <<[s.Message]>>");

            MessageTestClass sender = new MessageTestClass("LINQ Reporting Engine", "Hello World");
            BuildReport(doc, sender, "s", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("LINQ Reporting Engine says: Hello World\f", doc.GetText());
        }

        [Test]
        public void StringFormat()
        {
            Document doc = DocumentHelper.CreateSimpleDocument("<<[s.Name]:lower>> says: <<[s.Message]:upper>>, <<[s.Message]:caps>>, <<[s.Message]:firstCap>>");

            MessageTestClass sender = new MessageTestClass("LINQ Reporting Engine", "hello world");
            BuildReport(doc, sender, "s");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("linq reporting engine says: HELLO WORLD, Hello World, Hello world\f", doc.GetText());
        }

        [Test]
        public void NumberFormat()
        {
            Document doc = DocumentHelper.CreateSimpleDocument("<<[s.Value1]:alphabetic>> : <<[s.Value2]:roman:lower>>, <<[s.Value3]:ordinal>>, <<[s.Value1]:ordinalText:upper>>" + ", <<[s.Value2]:cardinal>>, <<[s.Value3]:hex>>, <<[s.Value3]:arabicDash>>");

            NumericTestClass sender = new NumericTestBuilder().WithValuesAndDate(1, 2.2, 200, null, DateTime.Parse("10.09.2016 10:00:00")).Build();

            BuildReport(doc, sender, "s");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("A : ii, 200th, FIRST, Two, C8, - 200 -\f", doc.GetText());
        }

        [Test]
        public void DataTableTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestDataTable.docx");

            BuildReport(doc, Common.GetContracts(), "Contracts");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestDataTable.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestDataTable.docx", MyDir + @"\Golds\ReportingEngine.TestDataTable Gold.docx"));
        }

        [Test]
        public void ProgressiveTotal()
        {
            Document doc = new Document(MyDir + "ReportingEngine.Total.docx");

            BuildReport(doc, Common.GetContracts(), "Contracts");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.Total.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.Total.docx", MyDir + @"\Golds\ReportingEngine.Total Gold.docx"));
        }

        [Test]
        public void NestedDataTableTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestNestedDataTable.docx");

            BuildReport(doc, Common.GetManagers(), "Managers");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestNestedDataTable.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestNestedDataTable.docx", MyDir + @"\Golds\ReportingEngine.TestNestedDataTable Gold.docx"));
        }

        [Test]
        public void ChartTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestChart.docx");
            
            BuildReport(doc, Common.GetManagers(), "managers");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestChart.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestChart.docx", MyDir + @"\Golds\ReportingEngine.TestChart Gold.docx"));
        }

        [Test]
        public void BubbleChartTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestBubbleChart.docx");
            
            BuildReport(doc, Common.GetManagers(), "managers");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestBubbleChart.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestBubbleChart.docx", MyDir + @"\Golds\ReportingEngine.TestBubbleChart Gold.docx"));
        }

        [Test]
        public void ConditionalExpressionForLeaveChartSeries()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestRemoveChartSeries.docx");

            int condition = 3;

            BuildReport(doc, new object[] { Common.GetManagers(), condition }, new[] { "managers", "condition" });

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestLeaveChartSeries.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestLeaveChartSeries.docx", MyDir + @"\Golds\ReportingEngine.TestLeaveChartSeries Gold.docx"));
        }

        [Test]
        public void ConditionalExpressionForRemoveChartSeries()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestRemoveChartSeries.docx");

            int condition = 2;

            BuildReport(doc, new object[] { Common.GetManagers(), condition }, new[] { "managers", "condition" });

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestRemoveChartSeries.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestRemoveChartSeries.docx", MyDir + @"\Golds\ReportingEngine.TestRemoveChartSeries Gold.docx"));
        }

        [Test]
        public void IndexOf()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestIndexOf.docx");
            
            BuildReport(doc, Common.GetManagers(), "Managers");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("The names are: John Smith, Tony Anderson, July James\f", doc.GetText());
        }

        [Test]
        public void IfElse()
        {
            Document doc = new Document(MyDir + "ReportingEngine.IfElse.docx");
            
            BuildReport(doc, Common.GetManagers(), "m");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("You have chosen 3 item(s).\f", doc.GetText());
        }

        [Test]
        public void IfElseWithoutData()
        {
            Document doc = new Document(MyDir + "ReportingEngine.IfElse.docx");
            
            BuildReport(doc, Common.GetEmptyManagers(), "m");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("You have chosen no items.\f", doc.GetText());
        }

        [Test]
        public void ExtensionMethods()
        {
            Document doc = new Document(MyDir + "ReportingEngine.ExtensionMethods.docx");
            
            BuildReport(doc, Common.GetManagers(), "Managers");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.ExtensionMethods.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.ExtensionMethods.docx", MyDir + @"\Golds\ReportingEngine.ExtensionMethods Gold.docx"));
        }

        [Test]
        public void Operators()
        {
            Document doc = new Document(MyDir + "ReportingEngine.Operators.docx");

            NumericTestClass testData = new NumericTestBuilder().WithValuesAndLogical(1, 2.0, 3, null, true).Build();

            ReportingEngine report = new ReportingEngine();
            report.KnownTypes.Add(typeof(NumericTestBuilder));
            report.BuildReport(doc, testData, "ds");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.Operators.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.Operators.docx", MyDir + @"\Golds\ReportingEngine.Operators Gold.docx"));
        }

        [Test]
        public void ContextualObjectMemberAccess()
        {
            Document doc = new Document(MyDir + "ReportingEngine.ContextualObjectMemberAccess.docx");
            
            BuildReport(doc, Common.GetManagers(), "Managers");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.ContextualObjectMemberAccess.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.ContextualObjectMemberAccess.docx", MyDir + @"\Golds\ReportingEngine.ContextualObjectMemberAccess Gold.docx"));
        }

        [Test]
        public void InsertDocumentDinamically()
        {
            Document template = DocumentHelper.CreateSimpleDocument("<<doc [src.Document]>>");

            DocumentTestClass doc = new DocumentTestBuilder().WithDocument(new Document(MyDir + "ReportingEngine.TestDataTable.docx")).Build();

            BuildReport(template, doc, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by document");
        }

        [Test]
        public void InsertDocumentDinamicallyByStream()
        {
            Document template = DocumentHelper.CreateSimpleDocument("<<doc [src.DocumentStream]>>");

            DocumentTestClass docStream = new DocumentTestBuilder().WithDocumentStream(new FileStream(mDocument, FileMode.Open, FileAccess.Read)).Build();

            BuildReport(template, docStream, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by stream");
        }

        [Test]
        public void InsertDocumentDinamicallyByBytes()
        {
            Document template = DocumentHelper.CreateSimpleDocument("<<doc [src.DocumentBytes]>>");

            DocumentTestClass docBytes = new DocumentTestBuilder().WithDocumentBytes(File.ReadAllBytes(MyDir + "ReportingEngine.TestDataTable.docx")).Build();

            BuildReport(template, docBytes, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void InsertDocumentDinamicallyByUri()
        {
            Document template = DocumentHelper.CreateSimpleDocument("<<doc [src.DocumentUri]>>");

            DocumentTestClass docUri = new DocumentTestBuilder().WithDocumentUri("http://www.snee.com/xml/xslt/sample.doc").Build();

            BuildReport(template, docUri, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(uri) Gold.docx"), "Fail inserting document by uri");
        }

        [Test]
        public void InsertImageDinamically()
        {
            Document template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Image]>>", ShapeType.TextBox);
            ImageTestClass image = new ImageTestBuilder().WithImage(SKBitmap.Decode(mImage)).Build();

            BuildReport(template, image, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void InsertImageDinamicallyByStream()
        {
            Document template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageStream]>>", ShapeType.TextBox);
            ImageTestClass imageStream = new ImageTestBuilder().WithImageStream(new FileStream(mImage, FileMode.Open, FileAccess.Read)).Build();

            BuildReport(template, imageStream, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void InsertImageDinamicallyByBytes()
        {
            Document template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageBytes]>>", ShapeType.TextBox);
            ImageTestClass imageBytes = new ImageTestBuilder().WithImageBytes(File.ReadAllBytes(mImage)).Build();

            BuildReport(template, imageBytes, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void InsertImageDinamicallyByUri()
        {
            Document template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageUri]>>", ShapeType.TextBox);
            ImageTestClass imageUri = new ImageTestBuilder().WithImageUri("http://joomla-aspose.dynabic.com/templates/aspose/App_Themes/V3/images/customers/americanexpress.png").Build();

            BuildReport(template, imageUri, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(uri) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void WithoutKnownType()
        {
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.Writeln("<<[new DateTime()]:”dd.MM.yyyy”>>");

            ReportingEngine engine = new ReportingEngine();
            Assert.That(() => engine.BuildReport(doc, ""), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void WorkWithKnownTypes()
        {
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”dd.MM.yyyy”>>");
            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”dd”>>");
            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”MM”>>");
            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”yyyy”>>");

            builder.Writeln("<<[new DateTime(2016, 1, 20).Month]>>");

            ReportingEngine engine = new ReportingEngine();
            engine.KnownTypes.Add(typeof(DateTime));
            engine.BuildReport(doc, "");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.KnownTypes.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.KnownTypes.docx", MyDir + @"\Golds\ReportingEngine.KnownTypes Gold.docx"));
        }

        [Test]
        [Ignore("WORDSNET-16258")]
        public void StretchImagefitHeight()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageStream] -fitHeight>>", ShapeType.TextBox);

            ImageTestClass imageStream = new ImageTestBuilder().WithImageStream(new FileStream(mImage, FileMode.Open, FileAccess.Read)).Build();
            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);
            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox 
                Assert.IsTrue(shape.ImageData.HasImage);

                // Assert that width is keeped and height is changed
                Assert.AreNotEqual(346.35, shape.Height);
                Assert.AreEqual(431.5, shape.Width);
            }

            dstStream.Dispose();
        }

        [Test]
        [Ignore("WORDSNET-16258")]
        public void StretchImagefitWidth()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageStream] -fitWidth>>", ShapeType.TextBox);

            ImageTestClass imageStream = new ImageTestBuilder().WithImageStream(new FileStream(mImage, FileMode.Open, FileAccess.Read)).Build();
            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);
            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox and 
                Assert.IsTrue(shape.ImageData.HasImage);

                // Assert that height is keeped and width is changed
                Assert.AreNotEqual(431.5, shape.Width);
                Assert.AreEqual(346.35, shape.Height);
            }

            dstStream.Dispose();
        }

        [Test]
        [Ignore("WORDSNET-16258")]
        public void StretchImagefitSize()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageStream] -fitSize>>", ShapeType.TextBox);

            ImageTestClass imageStream = new ImageTestBuilder().WithImageStream(new FileStream(mImage, FileMode.Open, FileAccess.Read)).Build();
            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);
            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox 
                Assert.IsTrue(shape.ImageData.HasImage);

                // Assert that height is changed and width is changed
                Assert.AreNotEqual(346.35, shape.Height);
                Assert.AreNotEqual(431.5, shape.Width);
            }

            dstStream.Dispose();
        }

        [Test]
        [Ignore("WORDSNET-16258")]
        public void StretchImagefitSizeLim()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.ImageStream] -fitSizeLim>>", ShapeType.TextBox);

            ImageTestClass imageStream = new ImageTestBuilder().WithImageStream(new FileStream(mImage, FileMode.Open, FileAccess.Read)).Build();
            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);
            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox 
                Assert.IsTrue(shape.ImageData.HasImage);

                // Assert that textbox size are equal image size
                Assert.AreEqual(346.35, shape.Height);
                Assert.AreEqual(258.54, shape.Width);
            }

            dstStream.Dispose();
        }

        [Test]
        public void WithoutMissingMembers()
        {
            DocumentBuilder builder = new DocumentBuilder();

            //Add templete to the document for reporting engine
            DocumentHelper.InsertBuilderText(builder, new[] { "<<[missingObject.First().id]>>", "<<foreach [in missingObject]>><<[id]>><</foreach>>" });

            //Assert that build report failed without "ReportBuildOptions.AllowMissingMembers"
            Assert.That(() => BuildReport(builder.Document, new DataSet(), "", ReportBuildOptions.None), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void WithMissingMembers()
        {
            DocumentBuilder builder = new DocumentBuilder();

            //Add templete to the document for reporting engine
            DocumentHelper.InsertBuilderText(builder, new[] { "<<[missingObject.First().id]>>", "<<foreach [in missingObject]>><<[id]>><</foreach>>" });

            BuildReport(builder.Document, new DataSet(), "", ReportBuildOptions.AllowMissingMembers);

            //Assert that build report success with "ReportBuildOptions.AllowMissingMembers"
            Assert.AreEqual(ControlChar.ParagraphBreak + ControlChar.ParagraphBreak + ControlChar.SectionBreak, builder.Document.GetText());
        }

        [Test]
        public void SetBackgroundColor()
        {
            Document doc = new Document(MyDir + "ReportingEngine.BackColor.docx");

            List<ColorItemTestClass> colors = new List<ColorItemTestClass>
            {
                new ColorItemTestBuilder().WithColor("Black", Color.Black).Build(),
                new ColorItemTestBuilder().WithColor("Red", Color.FromArgb(255, 0, 0)).Build(),
                new ColorItemTestBuilder().WithColor("Empty", Color.Empty).Build()
            };

            BuildReport(doc, colors, "Colors");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.BackColor.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.BackColor.docx", MyDir + @"\Golds\ReportingEngine.BackColor Gold.docx"));
        }

        private static void BuildReport(Document document, object dataSource, string dataSourceName, ReportBuildOptions reportBuildOptions)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.Options = reportBuildOptions;

            engine.BuildReport(document, dataSource, dataSourceName);
        }

        private static void BuildReport(Document document, object[] dataSource, string[] dataSourceName)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(document, dataSource, dataSourceName);
        }

        private static void BuildReport(Document document, object dataSource, string dataSourceName)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(document, dataSource, dataSourceName);
        }

        private static void BuildReport(Document document, object dataSource)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(document, dataSource);
        }
    }
}
