import {coverageType  } from "../model/coverage-type";
export class Policy {
    Id: number
    Name: string
    Description: string
    PolicyStartDate: Date
    CoverageTime: number
    Price: number
    RiskType: string
    CoverageType: coverageType

}