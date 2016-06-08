﻿using System;
using System.Collections.Generic;
using System.IO;
using AgGateway.ADAPT.ApplicationDataModel;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.Documents;
using AgGateway.ADAPT.ApplicationDataModel.Guidance;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using AgGateway.ADAPT.ApplicationDataModel.Notes;
using AgGateway.ADAPT.ApplicationDataModel.ReferenceLayers;
using AgGateway.ADAPT.ApplicationDataModel.Representations;
using AgGateway.ADAPT.ApplicationDataModel.Shapes;
using ProtoBuf;
using ProtoBuf.Meta;

namespace ADMPlugin
{
    public interface IProtobufSerializer
    {
        void Write<T>(string path, T content);
        void WriteSpatialRecords(string path, IEnumerable<SpatialRecord> spatialRecords);
        T Read<T>(string path);
        IEnumerable<SpatialRecord> ReadSpatialRecords(string path);
    }

    public class ProtobufSerializer : IProtobufSerializer
    {
        private readonly RuntimeTypeModel _model;

        public ProtobufSerializer()
        {
            _model = GetProtobufModel();

        }

        public void Write<T>(string path, T content)
        {
            var type = content.GetType();
            if (!type.Namespace.StartsWith("System") && !type.Namespace.StartsWith("AgGateway.ADAPT.ApplicationDataModel"))
            {
                var baseType = type.BaseType;
                Write(path, Convert.ChangeType(content, baseType));
            }
            else
            {
            Write<T>(path, content, _model);
        }
        }

        private void Write<T>(string path, T content, RuntimeTypeModel model)
        {
            using (var fileStream = File.Open(path, FileMode.Create))
            {
                model.Serialize(fileStream, content);
            }
        }

        public T Read<T>(string path)
        {
            return Read<T>(path, _model);
        }

        private T Read<T>(string path, RuntimeTypeModel model)
        {
            using (var fileStream = File.OpenRead(path))
            {
                return (T)model.Deserialize(fileStream, null, typeof(T));
            }
        }

        public void WriteSpatialRecords(string path, IEnumerable<SpatialRecord> spatialRecords)
        {
            using (var fileStream = File.Open(path, FileMode.Create))
            {
                foreach (var spatialRecord in spatialRecords)
                {
                    _model.SerializeWithLengthPrefix(fileStream, spatialRecord, typeof(SpatialRecord), PrefixStyle.Base128, 1);
                }
            }
        }

        public IEnumerable<SpatialRecord> ReadSpatialRecords(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                while (!IsEndOfStream(fileStream))
                {
                    var spatialRecord = new SpatialRecord();
                    _model.DeserializeWithLengthPrefix(fileStream, spatialRecord, typeof(SpatialRecord), PrefixStyle.Base128, 1);
                    yield return spatialRecord;
                }
            }
        }

        private bool IsEndOfStream(FileStream fileStream)
        {
            return fileStream.Position + 20 > fileStream.Length;
        }



        RuntimeTypeModel GetProtobufModel()
        {
            var model = RuntimeTypeModel.Create();

            // BEGINNING OF GENERATED CODE
            // This code is generated by running the GenerateCodeThatCreateProtobufModel() method in ProtobufContractGeneratorTest
            // (but you have to manually copy it here)
            model[typeof(CalibrationFactor)].Add(1, "Id");
            model[typeof(CalibrationFactor)].Add(2, "MeterId");
            model[typeof(CalibrationFactor)].Add(3, "TimeScopeIds");
            model[typeof(CalibrationFactor)].Add(4, "Value");
            model[typeof(CalibrationFactor)].Add(5, "OperationDataId");
            model[typeof(DataLogTrigger)].Add(6, "Id");
            model[typeof(DataLogTrigger)].Add(7, "DataLogMethod");
            model[typeof(DataLogTrigger)].Add(8, "DataLogDistanceInterval");
            model[typeof(DataLogTrigger)].Add(9, "DataLogTimeInterval");
            model[typeof(DataLogTrigger)].Add(10, "DataLogThresholdMinimum");
            model[typeof(DataLogTrigger)].Add(11, "DataLogThresholdMaximum");
            model[typeof(DataLogTrigger)].Add(12, "DataLogThresholdChange");
            model[typeof(DataLogTrigger)].Add(13, "ContextItems");
            model[typeof(DataLogTrigger)].Add(14, "LoggingLevel");
            model[typeof(DataLogTrigger)].Add(15, "Representation");
            model[typeof(DataLogTrigger)].Add(16, "SectionIds");
            model[typeof(DataLogTrigger)].Add(17, "MeterIds");
            model[typeof(WorkingData)].Add(476, "Id");
            model[typeof(WorkingData)].Add(477, "DeviceElementUseId");
            model[typeof(WorkingData)].Add(478, "Representation");
            model[typeof(WorkingData)].Add(479, "AppliedLatency");
            model[typeof(WorkingData)].Add(480, "ReportedLatency");
            model[typeof(WorkingData)].Add(481, "TriggerId");
            model[typeof(WorkingData)].AddSubType(482, typeof(EnumeratedWorkingData));
            model[typeof(EnumeratedWorkingData)].Add(483, "ValueCodes");
            model[typeof(Load)].Add(26, "Id");
            model[typeof(Load)].Add(27, "Description");
            model[typeof(Load)].Add(28, "TimeScopeIds");
            model[typeof(Load)].Add(29, "LoadNumber");
            model[typeof(Load)].Add(30, "LoadType");
            model[typeof(Load)].Add(31, "LoadQuality");
            model[typeof(Load)].Add(32, "DestinationIds");
            model[typeof(Document)].Add(33, "Id");
            model[typeof(Document)].Add(34, "ContextItems");
            model[typeof(Document)].Add(35, "CropIds");
            model[typeof(Document)].Add(36, "CropZoneIds");
            model[typeof(Document)].Add(37, "Description");
            model[typeof(Document)].Add(38, "EstimatedArea");
            model[typeof(Document)].Add(39, "FarmIds");
            model[typeof(Document)].Add(40, "FieldIds");
            model[typeof(Document)].Add(41, "GrowerId");
            model[typeof(Document)].Add(42, "Notes");
            model[typeof(Document)].Add(43, "PersonRoleIds");
            model[typeof(Document)].Add(44, "TimeScopeIds");
            model[typeof(Document)].Add(45, "Version");
            model[typeof(Document)].AddSubType(46, typeof(WorkRecord));
            model[typeof(WorkRecord)].Add(484, "LoggedDataIds");
            model[typeof(WorkRecord)].Add(485, "SummariesIds");
            model[typeof(WorkRecord)].AddSubType(47, typeof(LoggedData));
            model[typeof(LoggedData)].Add(48, "WorkItemIds");
            model[typeof(LoggedData)].Add(49, "Notes");
            model[typeof(LoggedData)].Add(50, "MachineId");
            model[typeof(LoggedData)].Add(51, "GuidanceAllocationIds");
            model[typeof(LoggedData)].Add(52, "FarmId");
            model[typeof(LoggedData)].Add(53, "FieldId");
            model[typeof(LoggedData)].Add(54, "CropZoneId");
            model[typeof(LoggedData)].Add(486, "PersonRoles");
            model[typeof(LoggedData)].Add(55, "OperationData");
            model[typeof(LoggedData)].Add(56, "SummaryId");
            model[typeof(LoggedData)].Add(487, "ContainerUseIDs");
            model[typeof(WorkingData)].AddSubType(488, typeof(NumericWorkingData));
            model[typeof(NumericWorkingData)].Add(489, "UnitOfMeasure");
            model[typeof(NumericWorkingData)].Add(490, "Values");
            model[typeof(OperationData)].Add(60, "Id");
            model[typeof(OperationData)].Add(63, "LoadId");
            model[typeof(OperationData)].Add(64, "OperationType");
            model[typeof(OperationData)].Add(65, "PrescriptionId");
            model[typeof(OperationData)].Add(66, "ProductId");
            model[typeof(OperationData)].Add(67, "VarietyLocatorId");
            model[typeof(OperationData)].Add(68, "WorkItemOperationId");
            model[typeof(OperationData)].Add(69, "MaxDepth");
            model[typeof(OperationData)].Add(70, "SpatialRecordCount");
            model[typeof(DeviceElementUse)].Add(491, "Id");
            model[typeof(DeviceElementUse)].Add(492, "DeviceConfigurationId");
            model[typeof(DeviceElementUse)].Add(493, "OperationDataId");
            model[typeof(DeviceElementUse)].Add(494, "Depth");
            model[typeof(DeviceElementUse)].Add(495, "Order");
            model[typeof(DeviceElementUse)].Add(496, "TotalDistanceTravelled");
            model[typeof(DeviceElementUse)].Add(497, "TotalElapsedTime");
            model[typeof(SectionSummary)].Add(79, "Id");
            model[typeof(SectionSummary)].Add(80, "SectionId");
            model[typeof(SectionSummary)].Add(81, "TotalDistanceTravelled");
            model[typeof(SectionSummary)].Add(82, "TotalElapsedTime");
            model[typeof(SpatialRecord)].Add(83, "Geometry");
            model[typeof(SpatialRecord)].Add(84, "Timestamp");
            model[typeof(SpatialRecord)].Add(85, "_meterValues");
            model[typeof(SpatialRecord)].Add(86, "_appliedLatencyValues");
            model[typeof(CompoundIdentifier)].Add(87, "ReferenceId");
            model[typeof(CompoundIdentifier)].Add(88, "UniqueIds");
            model[typeof(ContextItem)].Add(89, "ContextItemType");
            model[typeof(ContextItem)].Add(90, "Value");
            model[typeof(ContextItem)].Add(91, "ContextItems");
            model[typeof(ContextItem)].Add(498, "TimeScopes");
            model[typeof(TimeScope)].Add(95, "Id");
            model[typeof(TimeScope)].Add(96, "Description");
            model[typeof(TimeScope)].Add(499, "DateContext");
            model[typeof(TimeScope)].Add(500, "TimeStamp1");
            model[typeof(TimeScope)].Add(501, "TimeStamp2");
            model[typeof(TimeScope)].Add(502, "Location1");
            model[typeof(TimeScope)].Add(503, "Location2");
            model[typeof(TimeScope)].Add(504, "Duration");
            model[typeof(UniqueId)].Add(99, "Id");
            model[typeof(UniqueId)].Add(100, "CiTypeEnum");
            model[typeof(UniqueId)].Add(101, "Source");
            model[typeof(UniqueId)].Add(102, "SourceType");
            model[typeof(UnitOfMeasure)].Add(103, "Id");
            model[typeof(UnitOfMeasure)].Add(104, "Code");
            model[typeof(UnitOfMeasure)].Add(105, "Dimension");
            model[typeof(UnitOfMeasure)].Add(106, "IsReferenceForDimension");
            model[typeof(UnitOfMeasure)].Add(107, "Scale");
            model[typeof(UnitOfMeasure)].Add(108, "Offset");
            model[typeof(EnumerationMember)].Add(109, "Code");
            model[typeof(EnumerationMember)].Add(110, "Value");
            model[typeof(Representation)].Add(111, "Id");
            model[typeof(Representation)].Add(112, "CodeSource");
            model[typeof(Representation)].Add(113, "Code");
            model[typeof(Representation)].Add(114, "Description");
            model[typeof(Representation)].Add(115, "LongDescription");
            model[typeof(Representation)].AddSubType(116, typeof(EnumeratedRepresentation));
            model[typeof(EnumeratedRepresentation)].Add(117, "EnumeratedMembers");
            model[typeof(EnumeratedRepresentation)].Add(118, "RepresentationGroupId");
            model[typeof(EnumeratedRepresentationGroup)].Add(119, "Id");
            model[typeof(EnumeratedRepresentationGroup)].Add(120, "Description");
            model[typeof(RepresentationValue)].Add(121, "Code");
            model[typeof(RepresentationValue)].Add(122, "Designator");
            model[typeof(RepresentationValue)].Add(123, "Color");
            model[typeof(RepresentationValue)].AddSubType(124, typeof(EnumeratedValue));
            model[typeof(EnumeratedValue)].Add(125, "Representation");
            model[typeof(EnumeratedValue)].Add(126, "Value");
            model[typeof(NumericValue)].Add(127, "Value");
            model[typeof(NumericValue)].Add(128, "UnitOfMeasure");
            model[typeof(Representation)].AddSubType(129, typeof(NumericRepresentation));
            model[typeof(NumericRepresentation)].Add(130, "DecimalDigits");
            model[typeof(NumericRepresentation)].Add(131, "MinValue");
            model[typeof(NumericRepresentation)].Add(132, "MaxValue");
            model[typeof(NumericRepresentation)].Add(133, "Dimension");
            model[typeof(RepresentationValue)].AddSubType(134, typeof(NumericRepresentationValue));
            model[typeof(NumericRepresentationValue)].Add(135, "Representation");
            model[typeof(NumericRepresentationValue)].Add(136, "Value");
            model[typeof(NumericRepresentationValue)].Add(137, "UserProvidedUnitOfMeasure");
            model[typeof(Representation)].AddSubType(138, typeof(StringRepresentation));
            model[typeof(StringRepresentation)].Add(139, "MinCharacters");
            model[typeof(StringRepresentation)].Add(140, "MaxCharacters");
            model[typeof(RepresentationValue)].AddSubType(141, typeof(StringValue));
            model[typeof(StringValue)].Add(142, "Representation");
            model[typeof(StringValue)].Add(143, "Value");
            model[typeof(BoundingBox)].Add(324, "MinY");
            model[typeof(BoundingBox)].Add(325, "MinX");
            model[typeof(BoundingBox)].Add(326, "MaxY");
            model[typeof(BoundingBox)].Add(327, "MaxX");
            model[typeof(Shape)].Add(148, "Id");
            model[typeof(Shape)].Add(149, "Type");
            model[typeof(Shape)].AddSubType(150, typeof(LinearRing));
            model[typeof(LinearRing)].Add(151, "Points");
            model[typeof(Shape)].AddSubType(152, typeof(LineString));
            model[typeof(LineString)].Add(153, "Points");
            model[typeof(Shape)].AddSubType(154, typeof(MultiLineString));
            model[typeof(MultiLineString)].Add(155, "LineStrings");
            model[typeof(Shape)].AddSubType(156, typeof(MultiPoint));
            model[typeof(MultiPoint)].Add(157, "Points");
            model[typeof(Shape)].AddSubType(158, typeof(MultiPolygon));
            model[typeof(MultiPolygon)].Add(159, "Polygons");
            model[typeof(Shape)].AddSubType(160, typeof(Point));
            model[typeof(Point)].Add(161, "X");
            model[typeof(Point)].Add(162, "Y");
            model[typeof(Point)].Add(163, "Z");
            model[typeof(Shape)].AddSubType(164, typeof(Polygon));
            model[typeof(Polygon)].Add(165, "ExteriorRing");
            model[typeof(Polygon)].Add(166, "InteriorRings");
            model[typeof(ApplicationDataModel)].Add(167, "ProprietaryValues");
            model[typeof(ApplicationDataModel)].Add(168, "Catalog");
            model[typeof(ApplicationDataModel)].Add(169, "Documents");
            model[typeof(ApplicationDataModel)].Add(170, "ReferenceLayers");
            model[typeof(Documents)].Add(171, "WorkItems");
            model[typeof(Documents)].Add(172, "WorkItemOperations");
            model[typeof(Documents)].Add(173, "LoggedData");
            model[typeof(Documents)].Add(174, "Plans");
            model[typeof(Documents)].Add(175, "WorkOrders");
            model[typeof(Documents)].Add(176, "Recommendations");
            model[typeof(Documents)].Add(177, "GuidanceAllocations");
            model[typeof(Documents)].Add(178, "Summaries");
            model[typeof(Documents)].Add(179, "LoggedDataCatalog");
            model[typeof(Properties)].Add(180, "_properties");
            model[typeof(ProprietaryValue)].Add(181, "Key");
            model[typeof(ProprietaryValue)].Add(182, "Value");
            model[typeof(Catalog)].Add(183, "Brands");
            model[typeof(Catalog)].Add(184, "Connectors");
            model[typeof(Catalog)].Add(185, "ContactInfo");
            model[typeof(Catalog)].Add(186, "Containers");
            model[typeof(Catalog)].Add(187, "Crops");
            model[typeof(Catalog)].Add(188, "CropProtectionProducts");
            model[typeof(Catalog)].Add(189, "CropVarieties");
            model[typeof(Catalog)].Add(190, "CropZones");
            model[typeof(Catalog)].Add(191, "Description");
            model[typeof(Catalog)].Add(192, "EquipmentConfigs");
            model[typeof(Catalog)].Add(193, "Farms");
            model[typeof(Catalog)].Add(194, "FertilizerProducts");
            model[typeof(Catalog)].Add(195, "Fields");
            model[typeof(Catalog)].Add(196, "FieldBoundaries");
            model[typeof(Catalog)].Add(197, "Growers");
            model[typeof(Catalog)].Add(198, "GuidancePatterns");
            model[typeof(Catalog)].Add(199, "GuidanceGroups");
            model[typeof(Catalog)].Add(200, "Implements");
            model[typeof(Catalog)].Add(201, "ImplementModels");
            model[typeof(Catalog)].Add(202, "ImplementTypes");
            model[typeof(Catalog)].Add(203, "ImplementConfigurations");
            model[typeof(Catalog)].Add(204, "Ingredients");
            model[typeof(Catalog)].Add(205, "Machines");
            model[typeof(Catalog)].Add(206, "MachineModels");
            model[typeof(Catalog)].Add(207, "MachineSeries");
            model[typeof(Catalog)].Add(208, "MachineTypes");
            model[typeof(Catalog)].Add(209, "MachineConfigurations");
            model[typeof(Catalog)].Add(210, "Manufacturers");
            model[typeof(Catalog)].Add(211, "Persons");
            model[typeof(Catalog)].Add(212, "PersonRoles");
            model[typeof(Catalog)].Add(213, "Prescriptions");
            model[typeof(Catalog)].Add(214, "ProductMixes");
            model[typeof(Catalog)].Add(215, "TimeScopes");
            model[typeof(Catalog)].Add(505, "DeviceElements");
            model[typeof(Catalog)].Add(506, "DeviceModels");
            model[typeof(Catalog)].Add(507, "DeviceElementConfigurations");
            model[typeof(Catalog)].Add(508, "HitchPoints");
            model[typeof(MeteredValue)].Add(509, "Value");
            model[typeof(MeteredValue)].Add(510, "MeterId");
            model[typeof(OperationSummary)].Add(511, "OperationType");
            model[typeof(OperationSummary)].Add(512, "ProductId");
            model[typeof(OperationSummary)].Add(513, "WorkItemOperationId");
            model[typeof(OperationSummary)].Add(514, "Data");
            model[typeof(OperationSummary)].Add(515, "EquipmentConfigId");
            model[typeof(Summary)].Add(516, "SummaryData");
            model[typeof(Summary)].Add(517, "FarmIds");
            model[typeof(Summary)].Add(518, "FieldIds");
            model[typeof(Summary)].Add(519, "CropZoneIds");
            model[typeof(Summary)].Add(520, "PersonRoleIds");
            model[typeof(Summary)].Add(521, "MachineId");
            model[typeof(Summary)].Add(522, "Notes");
            model[typeof(Summary)].Add(523, "LoggedDataIds");
            model[typeof(Summary)].Add(524, "WorkItemIds");
            model[typeof(Summary)].Add(525, "OperationSummaries");
            model[typeof(Summary)].Add(526, "ContainerUse");
            model[typeof(DocumentCorrelation)].Add(216, "Id");
            model[typeof(DocumentCorrelation)].Add(217, "RelationshipType");
            model[typeof(DocumentCorrelation)].Add(218, "DocumentId");
            model[typeof(DocumentCorrelation)].Add(527, "OriginatingDocumentId");
            model[typeof(DocumentCorrelation)].Add(328, "TimeScopes");
            model[typeof(DocumentCorrelation)].Add(221, "PersonRoleIds");
            model[typeof(Document)].AddSubType(223, typeof(Plan));
            model[typeof(Plan)].Add(528, "WorkItemIds");
            model[typeof(Document)].AddSubType(224, typeof(Recommendation));
            model[typeof(Recommendation)].Add(529, "WorkItemIds");
            model[typeof(StatusUpdate)].Add(225, "Status");
            model[typeof(StatusUpdate)].Add(530, "Note");
            model[typeof(StatusUpdate)].Add(227, "TimeStamp");
            model[typeof(WorkItem)].Add(228, "Id");
            model[typeof(WorkItem)].Add(232, "Notes");
            model[typeof(WorkItem)].Add(234, "TimeScopeIds");
            model[typeof(WorkItem)].Add(233, "WorkItemPriority");
            model[typeof(WorkItem)].Add(235, "PeopleRoleIds");
            model[typeof(WorkItem)].Add(236, "GrowerId");
            model[typeof(WorkItem)].Add(237, "FarmId");
            model[typeof(WorkItem)].Add(238, "FieldId");
            model[typeof(WorkItem)].Add(239, "CropZoneId");
            model[typeof(WorkItem)].Add(531, "ConnectorId");
            model[typeof(WorkItem)].Add(241, "ReferenceLayerIds");
            model[typeof(WorkItem)].Add(242, "BoundaryId");
            model[typeof(WorkItem)].Add(243, "WorkItemOperationIds");
            model[typeof(WorkItem)].Add(244, "GuidanceAllocationIds");
            model[typeof(WorkItem)].Add(245, "StatusUpdates");
            model[typeof(WorkItem)].Add(246, "ParentDocumentId");
            model[typeof(WorkItemOperation)].Add(247, "Description");
            model[typeof(WorkItemOperation)].Add(248, "Id");
            model[typeof(WorkItemOperation)].Add(249, "OperationType");
            model[typeof(WorkItemOperation)].Add(250, "PrescriptionId");
            model[typeof(Document)].AddSubType(253, typeof(WorkOrder));
            model[typeof(WorkOrder)].Add(254, "StatusUpdates");
            model[typeof(WorkOrder)].Add(532, "WorkItemIds");
            model[typeof(GuidancePattern)].Add(255, "Id");
            model[typeof(GuidancePattern)].Add(256, "GuidancePatternType");
            model[typeof(GuidancePattern)].Add(257, "GpsSource");
            model[typeof(GuidancePattern)].Add(258, "OriginalEpsgCode");
            model[typeof(GuidancePattern)].Add(259, "Description");
            model[typeof(GuidancePattern)].Add(260, "SwathWidth");
            model[typeof(GuidancePattern)].Add(261, "PropagationDirection");
            model[typeof(GuidancePattern)].Add(262, "Extension");
            model[typeof(GuidancePattern)].Add(263, "NumbersOfSwathsLeft");
            model[typeof(GuidancePattern)].Add(264, "NumbersOfSwathsRight");
            model[typeof(GuidancePattern)].Add(265, "BoundingPolygon");
            model[typeof(GuidancePattern)].AddSubType(266, typeof(AbCurve));
            model[typeof(AbCurve)].Add(267, "NumberOfSegments");
            model[typeof(AbCurve)].Add(268, "Heading");
            model[typeof(AbCurve)].Add(269, "EastShiftComponent");
            model[typeof(AbCurve)].Add(270, "NorthShiftComponent");
            model[typeof(AbCurve)].Add(271, "Shape");
            model[typeof(GuidancePattern)].AddSubType(272, typeof(AbLine));
            model[typeof(AbLine)].Add(273, "A");
            model[typeof(AbLine)].Add(274, "B");
            model[typeof(AbLine)].Add(275, "Heading");
            model[typeof(AbLine)].Add(276, "EastShiftComponent");
            model[typeof(AbLine)].Add(277, "NorthShiftComponent");
            model[typeof(GuidancePattern)].AddSubType(278, typeof(MultiAbLine));
            model[typeof(MultiAbLine)].Add(279, "AbLines");
            model[typeof(GuidancePattern)].AddSubType(280, typeof(APlus));
            model[typeof(APlus)].Add(281, "Point");
            model[typeof(APlus)].Add(282, "Heading");
            model[typeof(GuidancePattern)].AddSubType(283, typeof(CenterPivot));
            model[typeof(CenterPivot)].Add(284, "StartPoint");
            model[typeof(CenterPivot)].Add(285, "EndPoint");
            model[typeof(CenterPivot)].Add(286, "Center");
            model[typeof(GuidanceAllocation)].Add(287, "Id");
            model[typeof(GuidanceAllocation)].Add(288, "GuidanceGroupId");
            model[typeof(GuidanceAllocation)].Add(533, "GuidanceShift");
            model[typeof(GuidanceAllocation)].Add(329, "TimeScopes");
            model[typeof(GuidanceGroup)].Add(294, "Id");
            model[typeof(GuidanceGroup)].Add(295, "Description");
            model[typeof(GuidanceGroup)].Add(296, "GuidancePatternIds");
            model[typeof(GuidanceGroup)].Add(297, "BoundingPolygon");
            model[typeof(GuidanceShift)].Add(298, "GuidanceGroupId");
            model[typeof(GuidanceShift)].Add(299, "GuidancePatterId");
            model[typeof(GuidanceShift)].Add(300, "EastShift");
            model[typeof(GuidanceShift)].Add(301, "NorthShift");
            model[typeof(GuidanceShift)].Add(302, "PropagationOffset");
            model[typeof(GuidanceShift)].Add(303, "TimeScopeIds");
            model[typeof(GuidancePattern)].AddSubType(304, typeof(Spiral));
            model[typeof(Spiral)].Add(305, "Shape");
            model[typeof(ContainerEquipmentAllocation)].Add(534, "ContainerId");
            model[typeof(ContainerEquipmentAllocation)].Add(535, "sectionId");
            model[typeof(ContainerType)].Add(536, "Id");
            model[typeof(ContainerType)].Add(537, "Active");
            model[typeof(ContainerType)].Add(538, "Capacity");
            model[typeof(ContainerType)].Add(539, "Description");
            model[typeof(ContainerType)].Add(540, "ContextItems");
            model[typeof(ContainerUse)].Add(541, "AmountUsed");
            model[typeof(ContainerUse)].Add(542, "ContainerId");
            model[typeof(ContainerUse)].Add(543, "ProductId");
            model[typeof(ContainerUse)].Add(544, "ContainerAction");
            model[typeof(ContainerUse)].Add(545, "TimeScopes");
            model[typeof(ContainerUse)].Add(546, "DocumentIds");
            model[typeof(Container)].Add(308, "ContextItems");
            model[typeof(Container)].Add(309, "Description");
            model[typeof(Container)].Add(310, "Id");
            model[typeof(Container)].Add(547, "ContainerUseType");
            model[typeof(Container)].Add(548, "ContainerType");
            model[typeof(Note)].Add(315, "Description");
            model[typeof(Note)].Add(316, "Value");
            model[typeof(Note)].Add(330, "TimeStamps");
            model[typeof(Note)].Add(318, "SpatialContext");
            model[typeof(ReferenceLayer)].Add(333, "Id");
            model[typeof(ReferenceLayer)].Add(334, "Description");
            model[typeof(ReferenceLayer)].Add(335, "LayerType");
            model[typeof(ReferenceLayer)].Add(336, "TimeScopes");
            model[typeof(ReferenceLayer)].Add(337, "BoundingPolygon");
            model[typeof(ReferenceLayer)].Add(338, "ContextItems");
            model[typeof(ReferenceLayer)].Add(339, "FieldIds");
            model[typeof(ReferenceLayer)].Add(340, "CropZoneIds");
            model[typeof(ReferenceLayer)].AddSubType(341, typeof(RasterReferenceLayer));
            model[typeof(RasterReferenceLayer)].Add(342, "Origin");
            model[typeof(RasterReferenceLayer)].Add(343, "RowCount");
            model[typeof(RasterReferenceLayer)].Add(344, "ColumnCount");
            model[typeof(RasterReferenceLayer)].Add(345, "CellWidth");
            model[typeof(RasterReferenceLayer)].Add(346, "CellHeight");
            model[typeof(RasterReferenceLayer)].Add(347, "EnumeratedRasterValues");
            model[typeof(RasterReferenceLayer)].Add(348, "StringRasterValues");
            model[typeof(RasterReferenceLayer)].Add(349, "NumericRasterValues");
            model[typeof(RasterReferenceLayer)].Add(549, "SpatialAttributes");
            model[typeof(ShapeLookup)].Add(350, "Shape");
            model[typeof(ShapeLookup)].Add(351, "SpatialAttribute");
            model[typeof(ReferenceLayer)].AddSubType(352, typeof(ShapeReferenceLayer));
            model[typeof(ShapeReferenceLayer)].Add(353, "ShapeLookups");
            model[typeof(SpatialAttribute)].Add(354, "Values");
            model[typeof(CropZone)].Add(368, "Id");
            model[typeof(CropZone)].Add(369, "TimeScopeIds");
            model[typeof(CropZone)].Add(370, "Description");
            model[typeof(CropZone)].Add(371, "FieldId");
            model[typeof(CropZone)].Add(372, "CropId");
            model[typeof(CropZone)].Add(373, "Area");
            model[typeof(CropZone)].Add(374, "BoundingRegion");
            model[typeof(CropZone)].Add(375, "BoundarySource");
            model[typeof(CropZone)].Add(376, "Notes");
            model[typeof(CropZone)].Add(377, "GuidanceGroupIds");
            model[typeof(CropZone)].Add(378, "ContextItems");
            model[typeof(Brand)].Add(379, "Id");
            model[typeof(Brand)].Add(380, "Description");
            model[typeof(Brand)].Add(381, "ManufacturerId");
            model[typeof(Brand)].Add(382, "ContextItems");
            model[typeof(Company)].Add(383, "Id");
            model[typeof(Company)].Add(384, "Name");
            model[typeof(Company)].Add(385, "ContactInfoId");
            model[typeof(Company)].Add(386, "ContextItems");
            model[typeof(Contact)].Add(387, "Number");
            model[typeof(Contact)].Add(388, "Type");
            model[typeof(ContactInfo)].Add(389, "Id");
            model[typeof(ContactInfo)].Add(390, "AddressLine1");
            model[typeof(ContactInfo)].Add(391, "AddressLine2");
            model[typeof(ContactInfo)].Add(392, "PoBoxNumber");
            model[typeof(ContactInfo)].Add(393, "PostalCode");
            model[typeof(ContactInfo)].Add(394, "City");
            model[typeof(ContactInfo)].Add(395, "StateOrProvince");
            model[typeof(ContactInfo)].Add(396, "Country");
            model[typeof(ContactInfo)].Add(397, "CountryCode");
            model[typeof(ContactInfo)].Add(398, "Contacts");
            model[typeof(ContactInfo)].Add(399, "Location");
            model[typeof(ContactInfo)].Add(400, "ContextItems");
            model[typeof(Facility)].Add(401, "Id");
            model[typeof(Facility)].Add(402, "CompanyId");
            model[typeof(Facility)].Add(403, "Description");
            model[typeof(Facility)].Add(404, "ContactInfo");
            model[typeof(Facility)].Add(405, "FacilityType");
            model[typeof(Facility)].Add(406, "ContextItems");
            model[typeof(Farm)].Add(407, "Id");
            model[typeof(Farm)].Add(408, "Description");
            model[typeof(Farm)].Add(409, "GrowerId");
            model[typeof(Farm)].Add(410, "ContactInfo");
            model[typeof(Farm)].Add(411, "TimeScopeIds");
            model[typeof(Farm)].Add(412, "ContextItems");
            model[typeof(Field)].Add(413, "Id");
            model[typeof(Field)].Add(414, "Description");
            model[typeof(Field)].Add(415, "FarmId");
            model[typeof(Field)].Add(416, "Area");
            model[typeof(Field)].Add(417, "ActiveBoundaryId");
            model[typeof(Field)].Add(418, "ContextItems");
            model[typeof(Field)].Add(419, "Slope");
            model[typeof(Field)].Add(420, "Aspect");
            model[typeof(Field)].Add(421, "SlopeLength");
            model[typeof(Field)].Add(422, "GuidanceGroupIds");
            model[typeof(Field)].Add(423, "TimeScopeIds");
            model[typeof(ValueType)].AddSubType(424, typeof(Enum));
            model[typeof(Enum)].AddSubType(425, typeof(GLNEnum));
            model[typeof(GpsSource)].Add(426, "SourceType");
            model[typeof(GpsSource)].Add(427, "EstimatedPrecision");
            model[typeof(GpsSource)].Add(428, "HorizontalAccuracy");
            model[typeof(GpsSource)].Add(429, "VerticalAccuracy");
            model[typeof(GpsSource)].Add(430, "NumberOfSatellites");
            model[typeof(GpsSource)].Add(431, "GpsUtcTime");
            model[typeof(Grower)].Add(432, "Id");
            model[typeof(Grower)].Add(433, "Name");
            model[typeof(Grower)].Add(434, "ContactInfo");
            model[typeof(Grower)].Add(435, "ContextItems");
            model[typeof(Location)].Add(436, "Position");
            model[typeof(Location)].Add(437, "ContextItems");
            model[typeof(Location)].Add(438, "GpsSource");
            model[typeof(Destination)].Add(439, "Id");
            model[typeof(Destination)].Add(440, "Description");
            model[typeof(Destination)].Add(441, "Location");
            model[typeof(Destination)].Add(442, "FacilityId");
            model[typeof(Manufacturer)].Add(443, "Id");
            model[typeof(Manufacturer)].Add(444, "Description");
            model[typeof(PermittedProduct)].Add(445, "Id");
            model[typeof(PermittedProduct)].Add(446, "TimeScopes");
            model[typeof(PermittedProduct)].Add(447, "GrowerId");
            model[typeof(PermittedProduct)].Add(448, "ProductId");
            model[typeof(PermittedProduct)].Add(449, "ContextItems");
            model[typeof(Person)].Add(450, "Id");
            model[typeof(Person)].Add(451, "FirstName");
            model[typeof(Person)].Add(452, "MiddleName");
            model[typeof(Person)].Add(453, "LastName");
            model[typeof(Person)].Add(454, "CombinedName");
            model[typeof(Person)].Add(455, "ContactInfoId");
            model[typeof(Person)].Add(456, "ContextItems");
            model[typeof(PersonRole)].Add(457, "Id");
            model[typeof(PersonRole)].Add(458, "PersonId");
            model[typeof(PersonRole)].Add(459, "Role");
            model[typeof(PersonRole)].Add(460, "GrowerId");
            model[typeof(PersonRole)].Add(461, "TimeScopes");
            model[typeof(PersonRole)].Add(462, "CompanyId");
            model[typeof(RasterData<EnumeratedRepresentation, EnumerationMember>)].Add(463, "Representation");
            model[typeof(RasterData<StringRepresentation, string>)].Add(464, "Representation");
            model[typeof(RasterData<NumericRepresentation, NumericValue>)].Add(465, "Representation");
            model[typeof(SerializableRasterData<string>)].Add(466, "values");
            model[typeof(SerializableRasterData<EnumerationMember>)].Add(467, "values");
            model[typeof(SerializableRasterData<NumericValue>)].Add(468, "values");
            model[typeof(SerializableRasterData<string>)].Add(469, "Representation");
            model[typeof(SerializableRasterData<EnumerationMember>)].Add(470, "Representation");
            model[typeof(SerializableRasterData<NumericValue>)].Add(471, "Representation");
            model[typeof(SerializableReferenceLayer)].Add(472, "ReferenceLayer");
            model[typeof(SerializableReferenceLayer)].Add(473, "StringValues");
            model[typeof(SerializableReferenceLayer)].Add(474, "EnumerationMemberValues");
            model[typeof(SerializableReferenceLayer)].Add(475, "NumericValueValues");
            // END OF GENERATED CODE:
            //
            //
            return model;
        }
    }

}
