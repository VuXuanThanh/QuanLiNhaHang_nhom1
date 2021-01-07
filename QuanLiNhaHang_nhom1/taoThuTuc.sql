create proc sp_getInforOfFood @MaHD varchar(15)
as
begin
	select CHITIETHOADON.MaMon, TenMon,SoLuong, MONAN.DonGia, GiamGia, ThanhTien
	from CHITIETHOADON inner join HOADON on CHITIETHOADON.MaHD = HOADON.MaHD
	inner join MONAN on MONAN.MaMon = CHITIETHOADON.MaMon
	where HOADON.MaHD=@MaHD
end

exec sp_getInforOfFood '162021_203526'


go

create proc sp_ThongKeHoaDonTheoNgay @ngaybatdau datetime, @ngayketthuc datetime
as
begin
	select HOADON.MaHD, NgayXuat, MaNV, MaKH, sum(ThanhTien) as TongTien
	from CHITIETHOADON inner join HOADON on HOADON.MaHD = CHITIETHOADON.MaHD
	group by HOADON.MaHD, NgayXuat, MaNV, MaKH
	having NgayXuat>=@ngaybatdau and NgayXuat<=@ngayketthuc
end

exec sp_ThongKeHoaDonTheoNgay '2021-01-07', '2021-01-08'

