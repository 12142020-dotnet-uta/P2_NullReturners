import { TestBed } from '@angular/core/testing';

import { EquipmentService } from './equipment.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('EquipmentService', () => {
  let service: EquipmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(EquipmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
