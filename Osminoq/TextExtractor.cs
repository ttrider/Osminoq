//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TTRider.Osminoq
//{
//    public abstract class TextExtractor :  Extractor
//    {
//        private IExtractorPartition currentPartition;
//        private Type currentPartitionDataType;
        
//        protected TextExtractor(IExtractorSettings settings)
//            :base(settings)
//        {
//        }

//        protected abstract string[] ExtractRecord();

//        protected RecordsetDefiniton CurrentPartition
//        {
//            get { return this.currentPartition; }
//            set
//            {
//                if (this.currentPartition != value)
//                {
//                    this.currentPartition = value;
//                    this.currentPartitionDataType = DataItemFactory.GetDataItemType(CurrentPartition);
//                }
                
//            }
//        }


//        public override IDataItem ExtractDataItem()
//        {
//            var buffer = this.ExtractRecord();
//            if (buffer == null)
//            {
//                return null;
//            }

//            if (this.currentPartitionDataType == null)
//            {
//                throw new InvalidOperationException("CurrentPartiton is not set");
//            }

//            var di = Activator.CreateInstance(this.currentPartitionDataType);
//            ((IDataItemInternal)di).Initialize(buffer);

//            return (IDataItem)di;
//        }

//    }
//}
