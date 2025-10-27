import { OfficeStatus } from "./office-status";

export interface CreateOfficeModel{
    address: string;
    registryPhoneNumber: string;
    status: OfficeStatus;
    image?: File;
}