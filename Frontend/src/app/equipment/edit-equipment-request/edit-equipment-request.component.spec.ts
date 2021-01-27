import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEquipmentRequestComponent } from './edit-equipment-request.component';

import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('EditEquipmentRequestComponent', () => {
  let component: EditEquipmentRequestComponent;
  let fixture: ComponentFixture<EditEquipmentRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule],
      declarations: [ EditEquipmentRequestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEquipmentRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
