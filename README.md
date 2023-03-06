# ShoppingPlanApi

Bu Proje katmanı asıl işlemlerin olduğu base katmandır.
-Projeye öncelikle user katmanından var olan kullanıcılarla veya yeni kullanıcı oluşturarak kayıt olunuz.
-Kayıt işleminden sonra Login Process servisi ile bir token alınız.
-bu alınan tokenı swagger authorize kısmından tanıtınız.
-Proje de son kullanıcılar alışveriş listesi ekleyip düzenleyip görüntüleyebilmektedir.
-Listelerin ayrıntılı bilgileri için ürünler kaydedip bu listeleri kategorilere ayırabilmektedir.
-Bu listeler tamamlandığında put işlemi ile tamamlandı olarak düzenlenen listeler event fırlatılarak başka bir dbye kaydedilir.
-Yine aynı şekilde bu eventler ile diğer dbden bu kayıtlar okanbilmektedir.
-Listeleri aynı zamanda redis ile cachede de depolayabilmekteyiz.
-Bu projede extension tanımlama, automap kullanımı, validation örnekleri, jwt token örnekleri, generic repository tanımlama gibi bir çok konuda örnekler bulabilirsiniz.
-Projede Command Query yapışanması değil küçük bir proje olduğu için tüm servis işlemleri işleyişleri kapsamı ile controllerlar altında düzenlenmiştir.

# ShoppingApiAdmin
Bu katman event fırlatılarak eklenen diğer dbnin işlemleri için açılmıştır.

# ShoppingPlan.UnitTest

Kategori ekleme işlemi ve shopping list için testler bulunmaktadır.

# Not:
Projeyi indirip db contextler altındaki connection stringleri ayarlayarak direk çalıştırıp kullanabilirsiniz. Sizin için db yi kuracak ve örnek data yazacaktır.

https://www.patika.dev/tr :)
