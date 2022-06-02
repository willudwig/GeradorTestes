﻿CREATE TABLE [dbo].[TBMATERIA] (
    [NUMERO]            INT           IDENTITY (1, 1) NOT NULL,
    [TITULO]            VARCHAR (200) NOT NULL,
    [DISCIPLINA_NUMERO] INT           NOT NULL,
    [SERIE]             VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK__TBMATERI__7500EDCA4A69CBD5] PRIMARY KEY CLUSTERED ([NUMERO] ASC),
    CONSTRAINT [FK_DISCIPLINAS] FOREIGN KEY ([DISCIPLINA_NUMERO]) REFERENCES [dbo].[TBDISCIPLINA] ([NUMERO])
);
