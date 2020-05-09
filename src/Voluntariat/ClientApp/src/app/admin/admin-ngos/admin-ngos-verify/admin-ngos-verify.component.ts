import { Component, OnInit } from '@angular/core';
import { NGO } from '../admin-ngos.models';
import { AdminNgosService } from '../admin-ngos.service'

@Component({
  selector: 'admin-ngos-verify',
  templateUrl: './admin-ngos-verify.component.html',
})

export class AdminNgosVerifyComponent implements OnInit {
  displayedColumns: string[] = ['nrCrt', 'name', 'status', 'createdBy'];
  dataSource = this.service.getAll();;
  constructor(private service: AdminNgosService) { }

  ngOnInit() {
  }

}
