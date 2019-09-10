动态获取存储过程返回结果方法。    
调用`DatabaseFacade.RunDynamicSP`或`DbContext.RunDynamicSP`调用存储过程，传入存储过程名称和参数，可以获取一个`DSPTable`对象。  
该对象包含存储过程返回的所有行对象DSPRow，每一行对象包含一些列DSPCell对象。在列对象中存储值的相关信息。   
对于某些特殊存储过程可能无法获取返回结果架构信息，会抛出CanNotGetSchemaException异常。 