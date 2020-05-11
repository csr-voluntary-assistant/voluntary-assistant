export class NGO {
    id: string;
    name: string;
    status: NGOStatus;
    createdBy: string;
    headquartersAddress: string;
    headquartersPhoneNumber: string;
    headquartersEmail: string;
    identificationNumber: string;
    website: string;
    categoryName: string;
    serviceName: string;
}

export enum NGOStatus {
    PendingVerification = 0,
    Verified = 1
}
