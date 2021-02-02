import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaysComponent } from './plays.component';

import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('PlaysComponent', () => {
  let component: PlaysComponent;
  let fixture: ComponentFixture<PlaysComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule],
      declarations: [ PlaysComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call deletePlay(play)', () => {
   component.deletePlay(component.play);
  });
});
