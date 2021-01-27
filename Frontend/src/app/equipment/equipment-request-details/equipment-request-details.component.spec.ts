import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentRequestDetailsComponent } from './equipment-request-details.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('EquipmentRequestDetailsComponent', () => {
  let component: EquipmentRequestDetailsComponent;
  let fixture: ComponentFixture<EquipmentRequestDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule],
      declarations: [ EquipmentRequestDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentRequestDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
