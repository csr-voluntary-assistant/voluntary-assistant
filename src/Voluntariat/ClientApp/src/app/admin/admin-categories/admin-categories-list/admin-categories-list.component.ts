import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { AdminCategoriesService } from '../admin-categories.service'
import { Category, CategoryStatus, AddedBy } from '../admin-categories.models';
import { AdminCategoriesDeleteComponent } from '../admin-categories-delete/admin-categories-delete.component';

@Component({
  selector: 'app-admin-categories-list',
  templateUrl: './admin-categories-list.component.html',
  styleUrls: ['./admin-categories-list.component.css']
})

export class AdminCategoriesListComponent implements OnInit {

    displayedColumns: string[] = ['nrCrt', 'name', 'description', 'status', 'addedBy', 'createdOn', 'modify-delete'];
    dataSource: Category[] = [];
    status = CategoryStatus;
    addedBy = AddedBy;

    constructor(private service: AdminCategoriesService, public dialog: MatDialog) { }

    ngOnInit() {
        this.service.getAll().subscribe((data: Category[]) => {
            this.dataSource = data;
        });
    }

    openDeleteDialog(id: string) {
        const dialogRef = this.dialog.open(AdminCategoriesDeleteComponent);
        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.service.deleteByID(id).subscribe(() => {
                    this.dataSource = this.dataSource.filter(item => item.id !== id);
                });
            }
        });
    }

}
