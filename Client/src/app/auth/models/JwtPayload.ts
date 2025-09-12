export interface JwtPayload {
    sub: string;
    name?: string; //ClaimTypes.Name in BE
    uid?: string;
}