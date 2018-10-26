namespace BXH.AutomatedTests.Api.Apigee.Data
{
    public class NoticeData
    {
        public static readonly string ShipNoticeXML = @"
<?xml version='1.0' encoding='UTF-8'?>
<ShipNotice xmlns='urn:cidx:names:specification:ces:schema:all:5:0' Version='5.0'>
	<Header>
		<ThisDocumentIdentifier>
			<DocumentIdentifier>0000000014697782</DocumentIdentifier>
		</ThisDocumentIdentifier>
		<ThisDocumentDateTime>
			<DateTime DateTimeQualifier='On'>2018-07-12T04:23:10</DateTime>
		</ThisDocumentDateTime>
		<From>
			<PartnerInformation>
				<PartnerName>Syngenta</PartnerName>
				<PartnerIdentifier Agency='GLN'>0702941000001</PartnerIdentifier>
			</PartnerInformation>
		</From>
		<To>
			<PartnerInformation>
				<PartnerName>Nutrien Ag Solutions, Inc.</PartnerName>
				<PartnerIdentifier Agency='GLN'>0741069012213</PartnerIdentifier>
			</PartnerInformation>
		</To>
	</Header>
	<ShipNoticeBody>
		<ShipNoticeProperties>
			<ShipmentIdentification>
				<DocumentReference>
					<DocumentIdentifier>40264603</DocumentIdentifier>
					<ReleaseNumber>PO51849887</ReleaseNumber>
				</DocumentReference>
			</ShipmentIdentification>
			<ShipDate>
				<DateTime DateTimeQualifier='On'>2018-07-12T00:00:00</DateTime>
			</ShipDate>
			<PurchaseOrderInformation>
				<DocumentReference>
					<DocumentIdentifier>PO51849887</DocumentIdentifier>
					<ReferenceItem>1</ReferenceItem>
					<ReleaseNumber>1750561-001</ReleaseNumber>
				</DocumentReference>
			</PurchaseOrderInformation>
			<TransportMethodCode Domain='UN-Rec-19'>3</TransportMethodCode>
			<DeliveryTerms>
				<DeliveryTermsCode Domain='Incoterms-2000'>FCA</DeliveryTermsCode>
				<DeliveryTermsLocation>ORIGIN</DeliveryTermsLocation>
			</DeliveryTerms>
			<ShipNoticeDate>
				<DateTime DateTimeQualifier='On'>2018-07-12T04:23:22</DateTime>
			</ShipNoticeDate>
		</ShipNoticeProperties>
		<ShipNoticePartners>
			<Buyer>
				<PartnerInformation>
					<PartnerName>Nutrien Ag Solutions, Inc.</PartnerName>
					<PartnerIdentifier Agency='GLN'>0741069012213</PartnerIdentifier>
				</PartnerInformation>
			</Buyer>
			<Seller>
				<PartnerInformation>
					<PartnerName>Syngenta</PartnerName>
					<PartnerIdentifier Agency='GLN'>0702941000001</PartnerIdentifier>
				</PartnerInformation>
			</Seller>
			<OtherPartner PartnerRole='ShipFrom'>
				<PartnerInformation>
					<PartnerName>Kennewick Fertilizer Operation</PartnerName>
					<PartnerIdentifier Agency='GLN'>0629072000123</PartnerIdentifier>
				</PartnerInformation>
			</OtherPartner>
			<OtherPartner PartnerRole='ShipTo'>
				<PartnerInformation>
					<PartnerName>CPS</PartnerName>
					<PartnerIdentifier Agency='GLN'>0741069008001</PartnerIdentifier>
					<AddressInformation>
						<AddressLine>108 N. Columbia</AddressLine>
						<CityName>Connell</CityName>
						<StateOrProvince>WA</StateOrProvince>
						<PostalCode>99326</PostalCode>
						<PostalCountry>US</PostalCountry>
					</AddressInformation>
				</PartnerInformation>
			</OtherPartner>
			<OtherPartner PartnerRole='Carrier'>
				<PartnerInformation>
					<PartnerName>CUSTOMER TRUCK</PartnerName>
					<PartnerIdentifier Agency='SCAC'>NA</PartnerIdentifier>
				</PartnerInformation>
			</OtherPartner>
		</ShipNoticePartners>
		<ShipNoticeDetails>
			<EquipmentDetails>
				<LineNumber>1</LineNumber>
				<NetWeight>
					<SpecifiedMeasurement MeasurementQualifier='EqualTo'>
						<Measurement>
							<MeasurementValue>64920</MeasurementValue>
							<UnitOfMeasureCode Domain='UN-Rec-20'>LBR</UnitOfMeasureCode>
						</Measurement>
					</SpecifiedMeasurement>
				</NetWeight>
				<TareWeight TareWeightQualifier='Actual'>
					<SpecifiedMeasurement MeasurementQualifier='EqualTo'>
						<Measurement>
							<MeasurementValue>32940</MeasurementValue>
							<UnitOfMeasureCode Domain='UN-Rec-20'>LBR</UnitOfMeasureCode>
						</Measurement>
					</SpecifiedMeasurement>
				</TareWeight>
				<GrossWeight>
					<SpecifiedMeasurement MeasurementQualifier='EqualTo'>
						<Measurement>
							<MeasurementValue>97860</MeasurementValue>
							<UnitOfMeasureCode Domain='UN-Rec-20'>LBR</UnitOfMeasureCode>
						</Measurement>
					</SpecifiedMeasurement>
				</GrossWeight>
				<NetVolume>
					<SpecifiedMeasurement MeasurementQualifier='EqualTo'>
						<Measurement>
							<MeasurementValue>97860</MeasurementValue>
							<UnitOfMeasureCode Domain='UN-Rec-20'>LBR</UnitOfMeasureCode>
						</Measurement>
					</SpecifiedMeasurement>
				</NetVolume>
			</EquipmentDetails>
			<ShipNoticeProductLineItem>
				<LineNumber>1</LineNumber>
				<EquipmentDetailsLineNumber>1</EquipmentDetailsLineNumber>
				<ProductIdentification>
					<ProductIdentifier Agency='AGIIS-ProductID'>00799211969847</ProductIdentifier>
					<ProductDescription>UAN SOLUTION 32-0-0</ProductDescription>
				</ProductIdentification>
				<ShippedQuantity>
					<Measurement>
						<MeasurementValue>150.00</MeasurementValue>
						<UnitOfMeasureCode Domain='UN-Rec-20'>EA</UnitOfMeasureCode>
					</Measurement>
				</ShippedQuantity>
				<PurchaseOrderInformation>
					<DocumentReference>
						<DocumentIdentifier>PO51849887</DocumentIdentifier>
						<ReleaseNumber>1750561-001</ReleaseNumber>
					</DocumentReference>
				</PurchaseOrderInformation>
				<ReferenceInformation ReferenceType='BillOfLadingNumber'>
					<DocumentReference>
						<DocumentIdentifier>40264603</DocumentIdentifier>
					</DocumentReference>
				</ReferenceInformation>
				<ReferenceInformation ReferenceType='ReleaseNumber'>
					<DocumentReference>
						<DocumentIdentifier>1750561-001</DocumentIdentifier>
					</DocumentReference>
				</ReferenceInformation>
				<ReferenceInformation ReferenceType='ContractNumber'>
					<DocumentReference>
						<DocumentIdentifier>0000160220</DocumentIdentifier>
					</DocumentReference>
				</ReferenceInformation>
			</ShipNoticeProductLineItem>
			<ShipNoticeProductLineItem>
				<LineNumber>2</LineNumber>
				<EquipmentDetailsLineNumber>2</EquipmentDetailsLineNumber>
				<ProductIdentification>
					<ProductIdentifier Agency='AGIIS-ProductID'>00799211969847</ProductIdentifier>
					<ProductDescription>UAN SOLUTION 32-0-0</ProductDescription>
				</ProductIdentification>
				<ShippedQuantity>
					<Measurement>
						<MeasurementValue>100.00</MeasurementValue>
						<UnitOfMeasureCode Domain='UN-Rec-20'>EA</UnitOfMeasureCode>
					</Measurement>
				</ShippedQuantity>
				<PurchaseOrderInformation>
					<DocumentReference>
						<DocumentIdentifier>PO51849887</DocumentIdentifier>
						<ReleaseNumber>1750561-001</ReleaseNumber>
					</DocumentReference>
				</PurchaseOrderInformation>
				<ReferenceInformation ReferenceType='BillOfLadingNumber'>
					<DocumentReference>
						<DocumentIdentifier>40264603</DocumentIdentifier>
					</DocumentReference>
				</ReferenceInformation>
				<ReferenceInformation ReferenceType='ReleaseNumber'>
					<DocumentReference>
						<DocumentIdentifier>1750561-001</DocumentIdentifier>
					</DocumentReference>
				</ReferenceInformation>
				<ReferenceInformation ReferenceType='ContractNumber'>
					<DocumentReference>
						<DocumentIdentifier>0000160220</DocumentIdentifier>
					</DocumentReference>
				</ReferenceInformation>
			</ShipNoticeProductLineItem>
		</ShipNoticeDetails>
	</ShipNoticeBody>
</ShipNotice>
";
    }
}