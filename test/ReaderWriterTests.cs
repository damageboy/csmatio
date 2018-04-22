using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using csmatio.io;
using csmatio.types;
using NUnit.Framework;

namespace test
{
    public class ReaderWriterTests
    {
        string _tempFileName;

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_tempFileName))
                File.Delete(_tempFileName);
        }

        [TestCaseSource(nameof(ReadDataFolder))]
        [Test]
        public void ReadTestData(string matFileName)
        {
            var reader = new MatFileReader(matFileName);
            Assert.That(reader.Data, Is.Not.Empty);
        }


        [TestCaseSource(nameof(ReadDataFolder))]
        [Test]
        public void RoundTrip(string matFileName)
        {
            var reader = new MatFileReader(matFileName);

            _tempFileName = Path.GetTempFileName();
            var writer = new MatFileWriter(_tempFileName, reader.Data, false);

            var roundTrip = new MatFileReader(_tempFileName);

            foreach (var (mla1, mla2) in reader.Data.Zip(roundTrip.Data, (mla1, mla2) => (mla1: mla1, mla2 :mla2)))
                Compare(mla1, mla2);
        }

        void Compare(MLArray mla1, MLArray mla2)
        {
	        Assert.That(mla1.Flags, Is.EqualTo(mla2.Flags));

	        Assert.That(mla1.Dimensions, Is.EqualTo(mla2.Dimensions));

	        Assert.That(mla1.Name, Is.EqualTo(mla2.Name));

	        Assert.That(mla1.Type, Is.EqualTo(mla2.Type));

			switch (mla1.Type)
			{
				case MLArray.mxCHAR_CLASS :
					var c1 = ((MLChar) mla1);
					var c2 = ((MLChar) mla2);
					break;
				case MLArray.mxDOUBLE_CLASS:
					CompareNumericArray((MLNumericArray<double>) mla1, (MLNumericArray<double>) mla2);
					break;
				case MLArray.mxSINGLE_CLASS:
					CompareNumericArray((MLNumericArray<float>) mla1, (MLNumericArray<float>) mla2);
					break;
				case MLArray.mxUINT8_CLASS:
					CompareNumericArray((MLNumericArray<byte>) mla1, (MLNumericArray<byte>) mla2);
					break;
				case MLArray.mxINT8_CLASS:
					CompareNumericArray((MLNumericArray<sbyte>) mla1, (MLNumericArray<sbyte>) mla2);
					break;
				case MLArray.mxUINT16_CLASS:
					CompareNumericArray((MLNumericArray<ushort>) mla1, (MLNumericArray<ushort>) mla2);
					break;
				case MLArray.mxINT16_CLASS:
					CompareNumericArray((MLNumericArray<short>) mla1, (MLNumericArray<short>) mla2);
					break;
				case MLArray.mxUINT32_CLASS:
					CompareNumericArray((MLNumericArray<uint>) mla1, (MLNumericArray<uint>) mla2);
					break;
				case MLArray.mxINT32_CLASS:
					CompareNumericArray((MLNumericArray<int>) mla1, (MLNumericArray<int>) mla2);
					break;
				case MLArray.mxUINT64_CLASS:
					CompareNumericArray((MLNumericArray<ulong>) mla1, (MLNumericArray<ulong>) mla2);
					break;
				case MLArray.mxINT64_CLASS:
					CompareNumericArray((MLNumericArray<long>) mla1, (MLNumericArray<long>) mla2);
					break;
				case MLArray.mxSTRUCT_CLASS:
					Assert.That(((MLStructure) mla1).GetKeySetToByteArray(), Is.EquivalentTo(((MLStructure) mla2).GetKeySetToByteArray()));
					foreach (var (a1, a2) in ((MLStructure)mla1).AllFields.Zip(((MLStructure)mla2).AllFields, (a1, a2) => (a1: a1, a2: a2)))
						Compare(a1, a2);
					break;
				case MLArray.mxCELL_CLASS:
					foreach (var (a1, a2) in ((MLCell)mla1).Cells.Cast<MLArray>().Zip(((MLCell)mla2).Cells.Cast<MLArray>(), (a1, a2) => (a1: a1, a2: a2)))
						Compare(a1, a2);
					break;
				case MLArray.mxSPARSE_CLASS:
//					int[] ai;
//
//					//write ir
//					buffer = new MemoryStream();
//					bufferBW = new BinaryWriter(buffer);
//					ai = ((MLSparse)array).IR;
//					foreach (var i in ai)
//					{
//						bufferBW.Write(i);
//					}
//					tag = new OSArrayTag(MatDataTypes.miINT32, buffer.ToArray());
//					tag.WriteTo(bw);
//
//					// write jc
//					buffer = new MemoryStream();
//					bufferBW = new BinaryWriter(buffer);
//					ai = ((MLSparse)array).JC;
//					foreach (var i in ai)
//					{
//						bufferBW.Write(i);
//					}
//					tag = new OSArrayTag(MatDataTypes.miINT32, buffer.ToArray());
//					tag.WriteTo(bw);
//
//					//write real
//					buffer = new MemoryStream();
//					bufferBW = new BinaryWriter(buffer);
//					var ad = ((MLSparse)array).ExportReal();
//					for (var i = 0; i < ad.Length; i++)
//					{
//						bufferBW.Write(ad[i]);
//					}
//					tag = new OSArrayTag(MatDataTypes.miDOUBLE, buffer.ToArray());
//					tag.WriteTo(bw);
//
//					//write imaginary
//					if (array.IsComplex)
//					{
//						buffer = new MemoryStream();
//						bufferBW = new BinaryWriter(buffer);
//						ad = ((MLSparse)array).ExportImaginary();
//						for (var i = 0; i < ad.Length; i++)
//						{
//							bufferBW.Write(ad[i]);
//						}
//						tag = new OSArrayTag(MatDataTypes.miDOUBLE, buffer.ToArray());
//						tag.WriteTo(bw);
//					}
					break;
				default:
					throw new Exception("Cannot compare matrix of type: " + MLArray.TypeToString(mla1.Type));
			}
        }

	    static void CompareNumericArray<T>(MLNumericArray<T> mna1, MLNumericArray<T> mna2)
	    {
		    Assert.That(mna1.IsComplex, Is.EqualTo(mna2.IsComplex));
		    Assert.That(mna1.RealByteBuffer.Array(), Is.EquivalentTo(mna2.RealByteBuffer.Array()));
		    if (mna1.IsComplex)
			    Assert.That(mna1.ImaginaryByteBuffer.Array(), Is.EquivalentTo(mna2.ImaginaryByteBuffer.Array()));
	    }


	    public static IEnumerable<TestCaseData> ReadDataFolder()
        {
#if NET471
            var baseDir = TestContext.CurrentContext.TestDirectory;
#elif NETCOREAPP2_0
             var baseDir = TestContext.CurrentContext.WorkDirectory;
#endif
            var dir = Path.Combine(baseDir, "..", "..", "..", "..", "data");


            foreach (var f in Directory.EnumerateFiles(dir, "*.mat"))
            {
                var tcd = new TestCaseData(f) {TestName = TestContext.CurrentContext.Test.MethodName + "." + Path.GetFileNameWithoutExtension(f)};
                yield return tcd;
            }
        }

    }
}
