import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AdminCategoriesService } from '../admin-categories.service'
import { Category, CategoryStatus, AddedBy } from '../admin-categories.models';

@Component({
  selector: 'app-admin-categories-edit',
  templateUrl: './admin-categories-edit.component.html',
  styleUrls: ['./admin-categories-edit.component.css']
})

export class AdminCategoriesEditComponent implements OnInit {
    id: string;
    status = CategoryStatus;
    addedBy = AddedBy;
    category: Category = new Category();

    constructor(private service: AdminCategoriesService, private actRoute: ActivatedRoute, private router: Router) { }

    ngOnInit(): void {
        this.actRoute.paramMap.subscribe(params => {
            this.id = params.get('id');
        });

        this.service.getByID(this.id).subscribe((data: Category) => {
            this.category = data;
        });
    }

    onSubmit() {
        this.service.modifyByID(this.id, this.category).subscribe(() => {
            this.router.navigate(['../../list'], { relativeTo: this.actRoute });
        });      
    }
}
