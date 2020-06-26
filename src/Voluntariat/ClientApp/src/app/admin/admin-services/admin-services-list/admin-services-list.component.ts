import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { AdminServicesService } from '../admin-services.service'
import { Service, ServiceStatus, AddedBy } from '../admin-services.models';
import { AdminServicesDeleteComponent } from '../admin-services-delete/admin-services-delete.component';

@Component({
  selector: 'app-admin-services-list',
  templateUrl: './admin-services-list.component.html',
  styleUrls: ['./admin-services-list.component.css']
})

export class AdminServicesListComponent implements OnInit {

    displayedColumns: string[] = ['nrCrt', 'name', 'description', 'status', 'addedBy', 'createdOn', 'modify-delete'];
    dataSource: Service[] = [];
    status = ServiceStatus;
    addedBy = AddedBy;

    constructor(private service: AdminServicesService, public dialog: MatDialog) { }

    ngOnInit() {
        this.service.getAll().subscribe((data: Service[]) => {
            this.dataSource = data;
        });
    }

    openDeleteDialog(id: string) {
        const dialogRef = this.dialog.open(AdminServicesDeleteComponent);
        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.service.deleteByID(id).subscribe(() => {
                    this.dataSource = this.dataSource.filter(item => item.id !== id);
                });
            }
        });
    }

}
