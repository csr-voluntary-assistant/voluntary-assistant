export class Service {
    id: string;
    name: string;
    description: string;
    serviceStatus: ServiceStatus;
    addedBy: AddedBy;
    createdOn: Date;
}

export enum ServiceStatus {
    Pending = 0,
    Approved = 1,
    Declined = 2
}

export enum AddedBy {
    PlatformAdmin = 0,
    NGO = 1
}


