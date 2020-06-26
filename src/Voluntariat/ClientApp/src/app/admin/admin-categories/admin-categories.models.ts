export class Category {
    id: string;
    name: string;
    description: string;
    categoryStatus: CategoryStatus;
    addedBy: AddedBy;
    createdOn: Date;
}

export enum CategoryStatus {
    Pending = 0,
    Approved = 1,
    Declined = 2
}

export enum AddedBy {
    PlatformAdmin = 0,
    NGO = 1
}
