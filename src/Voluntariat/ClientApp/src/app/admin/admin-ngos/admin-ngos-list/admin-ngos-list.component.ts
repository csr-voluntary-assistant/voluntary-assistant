import { Component, OnInit } from '@angular/core';
import { NGO } from '../admin-ngos.models';
import { AdminNgosService } from '../admin-ngos.service'

@Component({
  selector: 'admin-ngos-list',
  templateUrl: './admin-ngos-list.component.html',
})

export class AdminNgosListComponent implements OnInit {
  displayedColumns: string[] = ['nrCrt' ,'name', 'status', 'createdBy'];
  dataSource = this.service.getAll();;
  constructor(private service: AdminNgosService) { }

  ngOnInit() {
  }

}
