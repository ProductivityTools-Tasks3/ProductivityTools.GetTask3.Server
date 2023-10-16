CREATE FUNCTION UpdateList(@value AS NVARCHAR(Max))
RETURNS NVARCHAR(Max)
AS
BEGIN
DECLARE @ROW NVARCHAR(Max)
--SET @ROW='[{"type":"title","children":[{"text":"Kryształy combined"}]},{"type":"ul","children":[{"type":"list-item","children":[{"children":[{"text":"Kasia zrobiła fun event zony czas."}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Remy poszedł na oddanie krwi w czwartek. W piątek był sundar day. Zamiast jednego dnia wolnego dostał dwa dni (czyli jeden wolny dzień za krew przypada mu w wolny dzień ogólny) pyta mnie co z tym robimy. *Ale jak ja juz oddalem to dostalem swistek czyt zaswiadczenie usprawiedliwiajace moja nieobecnosc w dniu oddania krwi, jak i dnia nastepnego ( to tak jakbym dostal dwa dni wolnego i tak jakby pracowalem w piatek*)"}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Pawel po dwóch latach lock-downu chce aby wziaść pod uwagę że ma ciężkie warunki w domu do pracy podczas perfa. Nie chce przychodzić do biura bo się ‘boi’"}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Pierwszy temat jaki remy wrzucił mi na naszym 121 które było pierwsze od półtora miesiąca to żę za kubek który miał dostać za darmo zapłacił 25 zł cła"}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Pawel zalożył internet podobno tylko dla celów firmowych"}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"A report will be attending a conference remotely, and wants to stay at a Google hotel (go/stay) for the duration of the conference (a whole week) so that they can focus on the conference. Report argues that there are too many distractions at home, and that commuting to the office every day by shuttle (>1 hour each way) will be too draining because they would have to take the earliest and latest shuttles to not miss all the talks. Report normally comes into the office 3-4 times a week, but takes later shuttles in the morning."}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Alex przychodziła codziennie do pracy na 11. Ale jak miała shift który zaczynał się u niej o 8, to chciała dodatkowe pieniądze za pracę poza godzinami pracy."}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Rozmawiałem z nowym TSE w zespole, poszliśmy na lunch, pytam co tam ostatnio oglądał on że unboxing jakiegoś samsunga T32. Pytam co tam w tym samsungu fajnego, a on że troche lepszy niż T31. Rozmowa się zbytnio nie kleiła. "}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Miałem podane widły na kupno plecaka - racjonalnie, FTE przyszedł do mnie z pytaniem o plecak luis vitton"}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"FTE kupił za swoje pieniądze naklejki w sklepie i rozdał je ludziom w zespole. Inni managerowie przyszli do mnie bo ich ludzie narzekają że oni nie dostali naklejek"}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"MK: Ja myślę że, właśnie zdałem sobie sprawę jak zacząłem to mówić że nie wiem co myślę. "}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Dwóch chłopaków przyszło w weekend do pracy i ponieważ nie było jedzenia kupili sobie po obiedzie za 350 zł, kilka dań."}],"type":"list-item-text"}]},{"type":"list-item","children":[{"children":[{"text":"Jak w weekend można było zamawiać jedzenie do biura, to w dublinie ludzie zamawiali do biura przychodzili odebrać i wracali do domu"}],"type":"list-item-text"}]},{"children":[{"type":"list-item-text","children":[{"text":"Można reimbursować interned to 12 miesięcy wstecz. Atalyk wrzucił dwunastomiesięczny reimburse, ale załączył jedną fakturę i pyta czy trzeba wszystkie 12."}]}],"type":"list-item"},{"children":[{"children":[{"text":"Giga pyta Grzesia czy powinien się w tym roku ożenić."}],"type":"list-item-text"}],"type":"list-item"},{"children":[{"children":[{"text":""}],"type":"list-item-text"}],"type":"list-item"}]}]'
SET @ROW = @value

SET @ROW=REPLACE(@Row,',"type":"list-item-text"}]','')
SET @ROW=REPLACE(@Row,'"children":[{"children":[{"text":','"children":[{"text":')

DECLARE @SEARCHPHRASE NVARCHAR(Max)

SET @SEARCHPHRASE='"type":"list-item","children":'

DECLARE @LISTITEMPOS INT
SELECT @LISTITEMPOS=CHARINDEX(@SEARCHPHRASE,@ROW)

DECLARE @NEWFORMAT NVARCHAR(Max)
SET @NEWFORMAT='"type":"li","children":[{"type":"lic","children":'
DECLARE @BEFORE NVARCHAR(Max)
SET @BEFORE=@ROW
IF @LISTITEMPOS>0
	BEGIN
		
		SET @BEFORE=SUBSTRING(@ROW,0,@LISTITEMPOS)
		--PRINT @BEFORE
		SET @BEFORE=@BEFORE+@NEWFORMAT
		--PRINT @BEFORE

		DECLARE @CLOSINGELEMENT INT
		SELECT @CLOSINGELEMENT = CHARINDEX('"}]}',@ROW,@LISTITEMPOS+LEN(@SEARCHPHRASE))
		--PRINT @CLOSINGELEMENT

		DECLARE @CONTENT NVARCHAR(Max)
		SELECT @CONTENT=SUBSTRING(@ROW,@LISTITEMPOS+LEN(@SEARCHPHRASE),4+@CLOSINGELEMENT-(@LISTITEMPOS+LEN(@SEARCHPHRASE)))
		--PRINT @CONTENT

		SET @BEFORE=@BEFORE+@CONTENT+']}'

		DECLARE @RESTDOCUMENT NVARCHAR(Max)
		SELECT @RESTDOCUMENT=SUBSTRING(@ROW,@CLOSINGELEMENT+4,LEN(@ROW)-@CLOSINGELEMENT-3)
	--	PRINT 'RESTDOCUMENT'
		--PRINT @RESTDOCUMENT

		SET @BEFORE=@BEFORE+@RESTDOCUMENT
		SET @BEFORE=ISNULL(@BEFORE,@ROW)
	END
RETURN @BEFORE
end



	 -- Update [PTJournal].[j].[Page] set Content = REPLACE(Content, '"type":"list-item","children":', '"type":"li","children":[{"type":"lic","children":') where PageId=2993